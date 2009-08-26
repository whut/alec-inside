//---------------------------------------------------------------------------

#ifndef PlayersH
#define PlayersH
#include <vector>
#include "Battlefield.h"
//---------------------------------------------------------------------------

const int ROWS = 10;
const int COLUMNS = 10;


class Ship;
class BattlefieldDrawer;

class Player {
	public:
		typedef std::vector<Ship*>::const_iterator ShipIter;
		typedef BattlefieldDrawer Drawer;

		Player();
		virtual ~Player();
		//расставить корабли
		virtual void BaseShips() = 0;
		//кинуть бомбу
		virtual bool DropBomb( const Player* enemy ) = 0;
		// есть кто еще живой?
		bool HasLiveShips() const;
		// поле боя
		Battlefield* Field() const { return bf; }
	protected:
		// список кораблей игрока
		std::vector<Ship*> ships_;
		//проверить не затоплен ли корабль
		static void CheckShip( const Ship* s );
	private:
		//поле боя
		Battlefield* bf;
};

//Человек
class Human : public Player {
	public:
		void BaseShips();
		bool DropBomb( const Player* enemy );
	private:
		bool TryBaseShip( Ship* s ) const;
		static int RequestCoordinate( int maxValue, std::string coordName );
};

//Компьютер
class Computer : public Player {
	public:
		//принимает спиок квадратов противника
		Computer( Battlefield* enemyField );
		void BaseShips();
		bool DropBomb( const Player* enemy );
	private:
		//список необстрелянных квадратов противника
		std::vector<BattleSquare*> squares_;
		//список квадратов последнего обстрелянного корабля
		std::vector<BattleSquare*> lastShipSquares_;

		//удалить соседние клетки для square
		void RemoveAdjacentSquares( BattleSquare* square );
		//извлечь очередной квадрат из списка необстрелянных
		BattleSquare* FetchNextSquare();
		// выбрать один из двух итераторов
		BattleSquareIter ChooseIter( BattleSquareIter it1,
			BattleSquareIter it2 );
		// найти итератор
		BattleSquareIter Find( int x, int y );
};


#endif
