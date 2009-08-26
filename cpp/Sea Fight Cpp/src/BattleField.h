//---------------------------------------------------------------------------

#ifndef BattleFieldH
#define BattleFieldH
#include <vector>
//---------------------------------------------------------------------------
class Ship;
// Результат выстрела: { попал, мимо, еще раз }
enum ShootResult { srSuccess, srEmpty, srAgain };

// Квадрат поля боя
class BattleSquare {
	public:
		// был ли уже обстрелян
		bool IsBombed() const { return bombed_; }
		// есть ли в квадрате корабль
		bool HasShip() const { return ship_ != NULL; }
		// возвращает корабль или NULL
		Ship* GetShip() const { return ship_; }
		// x координата квадрата
		int X() const { return x_; }
		// у координата квадрата
		int Y() const { return y_; }
		// является ли квадрат соседним (или самим собой)
		bool IsAdjacentOrSelf( const BattleSquare* other ) const;
		// является ли квадрат соседним (исключая диагональные)
		bool IsAdjacentStraight( const BattleSquare* other ) const;
		// сбросить бомбу
		ShootResult DropBomb();
	private:
		// конструктор доступен только друзьям класса
		// принимает координаты квадрата
		BattleSquare( int x, int y ) : x_(x), y_(y), bombed_(false) {
			ship_ = NULL;
		}
		//поместить корабль в квадрат
		//только друзья могут делать это
		void SetShip( Ship* s ) { ship_ = s; }

		//копирование не требуется
		BattleSquare( const BattleSquare& other );
		BattleSquare& operator=( const BattleSquare& other );
		//корабль
		Ship* ship_;
		int x_;
		int y_;
		//обстрелян?
		bool bombed_;
		// Класс Поле Боя может создавать Квадраты
		friend class Battlefield;
};

typedef std::vector<BattleSquare*>::const_iterator BattleSquareConstIter;
typedef std::vector<BattleSquare*>::iterator BattleSquareIter;

//Поле Боя
class Battlefield {
	public:
		/* rowCount - число строк
		   columnCount - число столбцов */
		Battlefield( int rowCount, int columnCount );
		virtual ~Battlefield();
		// возвращает копию списка квадратов
		std::vector<BattleSquare*> GetSquares() const;
		//пальнуть
		ShootResult DropBomb( int x, int y );
		//разместить корабль s
		// x,y - координаты квадрата в который будет помещен нос корабля
		// возвращает успешность размещения
		bool BaseShip( int x, int y, Ship* s );

		int ColumnCount() const {
			return columnCount_;
		}
		int RowCount() const {
			return rowCount_;
		}

		bool SquareExists( int x, int y ) const;
		//возвращает итератор для квадрата
		BattleSquareConstIter GetSquare( int x, int y) const;
	private:
		int rowCount_;
		int columnCount_;
		//Квадраты
		std::vector<BattleSquare*> squares_;

		//копирование не требуется
		Battlefield( const Battlefield& other );
		Battlefield& operator=( const Battlefield& other );

		//возможно ли разместить корабль
		//нос корабля помещается в квадрат *headSquareIter
		bool AllowBaseShip( const Ship* s,
			BattleSquareConstIter headSquareIter ) const;
};


// Функциональный объект для поиска клеток по координатам
class SquareCoordFinder {
	public:
		SquareCoordFinder( int x, int y ) : x_( x ), y_( y ) { }
		bool operator()( const BattleSquare* square ) const {
			return square->X() == x_ && square->Y() == y_;
		}
	private:
		int x_;
		int y_;
};

// Функциональный объект для поиска соседних клеток с кораблями
class SquareAdjacentWithShipFinder {
	public:
		SquareAdjacentWithShipFinder( BattleSquare* square ) {
			square_ = square;
		}
		bool operator()( const BattleSquare* other ) const {
			if ( square_->IsAdjacentOrSelf( other ) && other->HasShip() ) {
				return true;
			}
			return false;
		}
	private:
		//относительно квадрата
		BattleSquare* square_;
};
// Функциональный объект для поиска соседних квадратов
// исключая (straightOnly = true) или включая диагональные
class SquareAdjacentFinder {
	public:
		SquareAdjacentFinder( BattleSquare* square, bool straightOnly )
			: straightOnly_( straightOnly ) {
			square_ = square;
		}
		bool operator()( const BattleSquare* other ) const {
			return ( straightOnly_ ? square_->IsAdjacentStraight( other ) :
				square_->IsAdjacentOrSelf( other ) );
		}
	private:
		//относительно квадрата
		BattleSquare* square_;
		bool straightOnly_;
};





#endif
