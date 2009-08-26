//---------------------------------------------------------------------------

#ifndef DrawerH
#define DrawerH
//---------------------------------------------------------------------------
class Battlefield;

//Рисователь поля
class BattlefieldDrawer {
	public:
		static void Draw( const Battlefield* field, bool self );
	private:
		BattlefieldDrawer() {}
		static void PrintLineNumbers( int count );
		static void PrintLine( int count );
};
#endif
