//---------------------------------------------------------------------------


#include <algorithm>
#include <functional>
#include <stdexcept>
#include <assert.h>

#pragma hdrstop

#include "Battlefield.h"
#include "ship.h"
#include "utils.h"
//---------------------------------------------------------------------------

//--- BattleSquare ---

bool BattleSquare::IsAdjacentOrSelf( const BattleSquare* other ) const {
	return
		( other->x_ >= ( x_ - 1 ) && other->x_ <= ( x_ + 1 ) ) &&
		( other->y_ >= ( y_ - 1 ) && other->y_ <= ( y_ + 1 ) );
}

bool BattleSquare::IsAdjacentStraight( const BattleSquare* other ) const {
	return (
		( ( other->x_ == ( x_ - 1 ) || other->x_ == ( x_ + 1 ) ) &&
			other->y_ == y_ ) ||
		( ( other->y_ == ( y_ - 1 ) || other->y_ == ( y_ + 1 ) ) &&
			other->x_ == x_ )
		);
}

ShootResult BattleSquare::DropBomb() {
	//если уже стреляли
	if ( bombed_ ) {
		//то не надо тратить патроны
		return srAgain;
	}
	bombed_ = true;
	// если есть корабль
	if ( HasShip() ) {
		// разрушить палубу
		ship_->DestroyDeck();
		return srSuccess;
	}
	//мимо
	return srEmpty;
}


//--- BattleField ---
Battlefield::Battlefield( int rowCount, int columnCount ) :
	rowCount_( rowCount ), columnCount_( columnCount ) {
	//создаем Квадраты
	for ( int squareIndex = 0; squareIndex < rowCount_ * columnCount_;
		  ++squareIndex ) {

		squares_.push_back( new BattleSquare( squareIndex % rowCount_,
			squareIndex / columnCount_ ) );
	}
}

Battlefield::~Battlefield() {
	// удаляем Квадраты
	std::for_each( squares_.begin(), squares_.end(), wipe<BattleSquare> );
}

std::vector<BattleSquare*> Battlefield::GetSquares() const {
	return squares_;
}

ShootResult Battlefield::DropBomb( int x, int y ) {
	return ( *GetSquare( x, y ) )->DropBomb();
}

bool Battlefield::BaseShip( int x, int y, Ship* s ) {
	//итератор носа корабля
	BattleSquareConstIter headSquareIt( GetSquare( x, y ) );
	//если нельзя разместить корабль
	if ( !AllowBaseShip( s, headSquareIt ) ) {
		return false;
	}

	//шаг итератора
	int step = ( s->GetAlign() == saHorizontal ? 1 : columnCount_ );
	//страховка от выхода за допустимый интервал
	assert( squares_.size() > ( x + columnCount_ * y ) +
		( s->DeckCount() - 1 ) * step );

	//размещаем корабль по квадратам
	BattleSquareConstIter it( headSquareIt );
	for( int deckIndex = 0; deckIndex < s->DeckCount();
		++deckIndex, it += step ) {

		(*it)->SetShip( s );
	}
	return true;
}

bool Battlefield::SquareExists( int x, int y ) const {
	return ( x < columnCount_ &&  y < rowCount_ );
}

BattleSquareConstIter Battlefield::GetSquare( int x, int y) const {
	if ( !SquareExists( x, y ) ) {
		throw std::out_of_range( "Square not exists." );
	}
	return squares_.begin() + ( x + columnCount_ * y );
}

bool Battlefield::AllowBaseShip( const Ship* s,
	BattleSquareConstIter headSquareIter ) const {

	//входит ли корма в поле боя
	int shipSternX = s->Width() + (*headSquareIter)->X() - 1;
	int shipSternY = s->Height() + (*headSquareIter)->Y() - 1;
	if ( !SquareExists( shipSternX , shipSternY ) ) {
		return false;
	}

	//есть ли в зоне размещения корабли

	//шаг итератора
	int step = ( s->GetAlign() == saHorizontal ? 1 : columnCount_ );
	//страховка от выхода за допустимый интервал
	assert( squares_.size() >
		( (*headSquareIter)->X() + columnCount_ * (*headSquareIter)->Y() ) +
			( s->DeckCount() - 1 ) * step
	);

	//ищем квадраты с кораблями в зоне размещения
	BattleSquareConstIter it( headSquareIter );
	for( int deckIndex = 0; deckIndex < s->DeckCount();
		++deckIndex, it += step ) {

		if ( std::find_if( squares_.begin(), squares_.end(),
				SquareAdjacentWithShipFinder( *it ) ) !=
			 squares_.end()
			) {

			return false;
		}
	}
	return true;
}

