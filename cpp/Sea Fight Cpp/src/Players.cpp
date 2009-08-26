//---------------------------------------------------------------------------
#include <algorithm>
#include <limits>
#include <functional>
#include <assert.h>
#pragma hdrstop
#include "utils.h"

#include "Ship.h"
#include "Drawer.h"
#include "Players.h"
//---------------------------------------------------------------------------

// --- Player ---
Player::Player() {
	//создаем поле бо€
	bf = new Battlefield( ROWS, COLUMNS  );
	//заполн€ем корабли
	ships_.push_back( new Ship( 1 ) );
	ships_.push_back( new Ship( 1 ) );
	ships_.push_back( new Ship( 1 ) );
	ships_.push_back( new Ship( 2 ) );
	ships_.push_back( new Ship( 2 ) );
	ships_.push_back( new Ship( 3 ) );
	ships_.push_back( new Ship( 3 ) );
	ships_.push_back( new Ship( 4 ) );
}
Player::~Player() {
	//удал€ем корабли
	std::for_each( ships_.begin(), ships_.end(), wipe<Ship> );
	//удал€ем поле бо€
	delete bf;
}
bool Player::HasLiveShips() const {
	//ищем живой корабль
	return ( std::find_if( ships_.begin(), ships_.end(),
		std::mem_fun( &Ship::IsLive ) ) != ships_.end() );
}
void Player::CheckShip( const Ship* s ) {
	if ( !s->IsLive() ) {
		std::cout << "«атоплен " << s->DeckCount() <<
			"-палубный корабль" << std::endl;
	}
}

// --- Human ---

void Human::BaseShips() {
	std::cout << "–асставьте ваши корабли. "
		" орабли плывут справа налево и снизу вверх." << std::endl;
	Drawer::Draw( Field(), true );
	for ( ShipIter it( ships_.begin() ); it != ships_.end(); ++it ) {
		while ( !TryBaseShip( *it ) );
		Drawer::Draw( Field(), true );
	}
}
bool Human::DropBomb( const Player* enemy ) {
	Drawer::Draw( Field(), true );
	Drawer::Draw( enemy->Field(), false );

	std::cout << "“вой ход. —трел€й!" << std::endl;
	int x = RequestCoordinate( enemy->Field()->ColumnCount(), "X: " );
	int y = RequestCoordinate( enemy->Field()->RowCount(), "Y: " );
	switch( enemy->Field()->DropBomb( x, y ) ) {
		case srAgain:
			std::cout << "“ы туда уже стрел€л. Ќе трать патроны!" <<
				std::endl;
			return DropBomb( enemy );
		case srEmpty:
			std::cout << "ћазила." << std::endl;
			break;
		case srSuccess:
			std::cout << "’ороший выстрел. ѕопал!" << std::endl;
			CheckShip( ( *( enemy->Field()->GetSquare( x, y ) ) )->GetShip() );
			if ( !enemy->HasLiveShips() ) {
				std::cout << "ѕоздравл€ю, ты выиграл!" << std::endl;
				return true;
			}
			return DropBomb( enemy );
	}
	return false;
}
bool Human::TryBaseShip( Ship* s ) const {
	std::cout << "–азместите " << s->DeckCount() <<
		"-палубный корабль." << std::endl;
	if ( s->DeckCount() > 1 ) {
		int align = Console::Request<int>( "ќриентаци€ корабл€ "
			"(0 - горизонтальна€, 1 - вертикальна€):" );
		s->SetAlign( ShipAlign( align == 0 ? 0 : 1 ) );
	}

	int x = RequestCoordinate( Field()->ColumnCount(),
		"X координата носа:" );
	int y = RequestCoordinate( Field()->RowCount(),
		"Y координата носа:" );

	if ( !Field()->BaseShip( x, y, s ) ) {
		std::cout << "«десь нельз€ размещать корабль. "
			"ѕопробуйте снова." << std::endl;
		return false;
	}
	return true;
}
int Human::RequestCoordinate( int maxValue, std::string coordName ) {
	int coord = 0;
	do {
		if ( coord != 0 ) {
			std::cout << "Ќеверна€ координата. ¬ведите снова." <<
				std::endl;
		}
		coord = Console::Request<int>( coordName );
	}
	while( coord < 0 || coord >= maxValue );
	return coord;
}


// --- Computer ---
Computer::Computer( Battlefield* enemyField )
	: Player(), squares_( enemyField->GetSquares() ) {
}
void Computer::BaseShips( ) {
	for ( ShipIter it( ships_.begin() ); it != ships_.end(); ++it ) {
		int x, y;
		do {
			x = RandomRange( 0, Field()->ColumnCount() - 1 );
			y = RandomRange( 0, Field()->RowCount() - 1 );
			(*it)->SetAlign( ShipAlign( RandomRange( 0, 1 ) ) );
		}
		while( !Field()->BaseShip( x, y, *it ) );
	}
}
bool Computer::DropBomb( const Player* enemy ) {
	std::cout << "я хожу." << std::endl;

	BattleSquare* square = FetchNextSquare();

	std::cout << "—трел€ю так: ’= " << square->X() << " , Y= " <<
		square->Y() << std::endl;

	switch( square->DropBomb() ) {
		case srEmpty:
			std::cout << "ѕромазал." << std::endl;
			break;
		case srSuccess:
			std::cout << "я попал!" << std::endl;
			CheckShip( square->GetShip() );
			if ( !enemy->HasLiveShips() ) {
				std::cout << "» все-таки € выиграл! ¬ыпей вина! :P" <<
					std::endl;
				return true;
			}

			lastShipSquares_.push_back( square );
			//если корабль убит
			if ( !square->GetShip()->IsLive() ) {
				//удалить все соседние клетки
				for( BattleSquareConstIter it ( lastShipSquares_.begin() ) ;
					it != lastShipSquares_.end(); ++it ) {
					RemoveAdjacentSquares( *it );
				}
				lastShipSquares_.clear();
			}
			return DropBomb( enemy );
	}
	return false;
}


//удалить соседние клетки дл€ square
void Computer::RemoveAdjacentSquares( BattleSquare* square ) {
	squares_.erase(
		std::remove_if( squares_.begin(), squares_.end(),
			SquareAdjacentFinder( square, false ) ) ,
		squares_.end()
	);
}
//извлечь очередной квадрат из списка необстрел€нных
BattleSquare* Computer::FetchNextSquare() {
	//если нет обстреливаемого корабл€
	if ( lastShipSquares_.empty() ) {
		//берем случайный квадрат
		int squareIndex = RandomRange( 0, squares_.size() - 1 );
		BattleSquare* square = squares_[ squareIndex ];
		//удал€ем его из списка
		squares_.erase( squares_.begin() + squareIndex );
		return square;
	}
	// если попали один раз
	else if ( lastShipSquares_.size() == 1 ) {
		int x = lastShipSquares_[0]->X();
		int y = lastShipSquares_[0]->Y();
		//из доступных квадратов выбираем случайно
		//или не случайно если доступный квадрат один
		BattleSquareIter itResult( ChooseIter(
			ChooseIter( Find( x - 1, y ) , Find( x + 1, y ) ),
			ChooseIter( Find( x, y - 1 ) , Find( x, y + 1 ) ))
		);
		assert( itResult != squares_.end() );
		BattleSquare* square = *itResult;
		squares_.erase( itResult );
		return square;
	}
	//если попали более чем 1 раз
	else {
		//то нам известна ориентаци€ корабл€
		ShipAlign align = lastShipSquares_[0]->GetShip()->GetAlign();

		if ( align == saHorizontal ) {
			int Y = lastShipSquares_[0]->Y();
			int minX = lastShipSquares_[0]->X();
			int maxX = 0;
			//вычисл€ем ’-ы граничных квадратов
			for ( BattleSquareConstIter it( lastShipSquares_.begin() ) ;
				it != lastShipSquares_.end(); ++it ) {
				maxX = std::max( (*it)->X(), maxX );
				minX = std::min( (*it)->X(), minX );
			}
			//выбираем квадрат слева или справа случайно, либо тот который
			//есть в списке
			BattleSquareIter itResult(
				ChooseIter( Find( minX - 1, Y ), Find( maxX + 1, Y ) ) );
			assert( itResult != squares_.end() );
			BattleSquare* square = *itResult;
			squares_.erase( itResult );
			return square;
		}
		else {
			int X = lastShipSquares_[0]->X();
			int minY = lastShipSquares_[0]->Y();
			int maxY = 0;
			for ( BattleSquareConstIter it( lastShipSquares_.begin() ) ;
				it != lastShipSquares_.end(); ++it ) {
				maxY = std::max( (*it)->Y(), maxY );
				minY = std::min( (*it)->Y(), minY );
			}
			BattleSquareIter itResult(
				ChooseIter( Find( X, minY - 1 ), Find( X , maxY + 1 ) ) );

			BattleSquare* square = *itResult;
			squares_.erase( itResult );
			return square;
		}
	}
}

BattleSquareIter Computer::ChooseIter( BattleSquareIter it1,
	BattleSquareIter it2 ) {

	if ( it1 != squares_.end() && it2 != squares_.end() ) {
			return  ( RandomRange( 0, 1 ) == 1 ? it1 : it2 );
	}
	else if ( it1 != squares_.end() ) {
			return it1;
	}
	return it2;
}

BattleSquareIter Computer::Find( int x, int y ) {
	return std::find_if( squares_.begin(), squares_.end(),
				SquareCoordFinder( x, y ) );
}




