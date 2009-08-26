//---------------------------------------------------------------------------

#include <iostream>

#pragma hdrstop

#include "Battlefield.h"
#include "Drawer.h"
#include "Ship.h"
//---------------------------------------------------------------------------

void BattlefieldDrawer::Draw( const Battlefield* field, bool self ) {
	PrintLineNumbers( field->ColumnCount() );
	for ( int rowIndex = 0; rowIndex < field->RowCount(); ++rowIndex ) {
		PrintLine( field->ColumnCount() );
		std::cout << rowIndex << " ";
		for ( int colIndex = 0; colIndex < field->ColumnCount(); ++colIndex ) {
			std::cout << "|";
			BattleSquare* square = *(field->GetSquare( colIndex, rowIndex ));
			if ( square->HasShip() && !square->GetShip()->IsLive() ) {
            	std::cout << "V";
			}
			else if ( square->HasShip() && square->IsBombed() ) {
				std::cout << "X";
			}
			else if ( square->HasShip() && self ) {
				std::cout << "S";
			}
			else if ( square->IsBombed() ) {
				std::cout << "O";
			}
			else {
				std::cout << " ";
			}
		}
		std::cout << "|";
		std::cout << " " <<rowIndex;
		std::cout << std::endl;
	}
	PrintLine( field->ColumnCount() );
	PrintLineNumbers( field->ColumnCount() );
}

void BattlefieldDrawer::PrintLineNumbers( int count ) {
	std::cout << "  ";
	for ( int colIndex = 0; colIndex < count; ++colIndex ) {
		std::cout << " " << colIndex ;
	}
	std::cout << std::endl;
}
void BattlefieldDrawer::PrintLine( int count ) {
	std::cout << "  ";
	for ( int colIndex = 0; colIndex < count; ++colIndex ) {
		std::cout << "+-";
	}
	std::cout << "+" << std::endl;
}

