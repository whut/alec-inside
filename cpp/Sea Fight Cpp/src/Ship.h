#ifndef shipH
#define shipH

// Тип ориентации корабля
enum ShipAlign { saHorizontal, saVertical };

// Корабль
class Ship {
	public:
		/*
		   deckCount - число палуб;
		   align - ориентация корабля
		*/
		Ship( int deckCount, ShipAlign align = saHorizontal ) :
			deckCount_( deckCount ), liveDeckCount_( deckCount ),
			align_( align )  {
		}
		// число палуб корабля
		int DeckCount() const {
			return deckCount_;
		}
		// возвращает ориентацию корабля
		ShipAlign GetAlign() const {
			return align_;
		}
		// устанавливает ориентацию корабля
		void SetAlign( ShipAlign align ) {
			align_ = align;
		}
		// число клеток занимаемых кораблем по горизонали
		int Width() const {
			return ( align_ == saHorizontal ? deckCount_ : 1 );
		}
		// число клеток занимаемых кораблем по вертикали
		int Height() const {
			return ( align_ == saVertical ? deckCount_ : 1 );
		}
		// корабль "жив" или затоплен
		bool IsLive() const {
			return liveDeckCount_ > 0;
		}
		// уничтожить палубу
		void DestroyDeck() {
			--liveDeckCount_;
        }
	private:
		// палубы корабля
		int deckCount_;
		// живые палубы
		int liveDeckCount_;
		// ориентация корабля
		ShipAlign align_;
		// копирование не требуется
		Ship( const Ship& other );
		Ship& operator=( const Ship& );
};
#endif