//---------------------------------------------------------------------------

#include <iostream>
#include <cstdlib>
#include <memory>
#include "cp866cvt.h"
#pragma hdrstop

#include "Players.h"

//---------------------------------------------------------------------------

int main(int argc, char* argv[]) {
	//чтобы на консоль выводились русские буквы
	std::cout.imbue(std::locale(std::locale(".866"), new rus_codecvt() ));

	std::auto_ptr<Human> man( new Human() );
	std::auto_ptr<Computer> comp( new Computer( man->Field() ) );

	comp->BaseShips();
	man->BaseShips();
	//очистка консоли
	std::system( "cls" );

	std::cout << "Бой начался!" << std::endl;
	while( !man->DropBomb( comp.get() ) && !comp->DropBomb( man.get() ) );

	return 0;
}

//---------------------------------------------------------------------------
