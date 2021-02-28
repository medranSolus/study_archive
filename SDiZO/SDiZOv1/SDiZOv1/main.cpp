#include "pch.h"
#include <cstdlib>
#include <ctime>
#include <fstream>
#include <sstream>
#include <vector>
#include <list>
#include "DataTypes.h"
using namespace DataTypes;
using std::cout;
using std::vector;
using std::list;

///<summary>
///Rozmiar struktur dla czynnoœci testowych
///</summary>
const long TEST_SIZE = 20000;

///<summary>
///G³ówne menu programu
///</summary>
void mainMenu();
///<summary>
///Menu programu dla drzew
///</summary>
void treeMenu(short type, ITree * dataStructure = nullptr);
///<summary>
///Menu programu dla interfejsu IArray
///</summary>
void arrayMenu(short type, IArray * dataStructure = nullptr);
///<summary>
///Sprawdza czy instnieje plik
///</summary>
bool fileExist(const std::string & fileName);
///<summary>
///Generuje dane dla struktur
///</summary>
bool generateData(long count = 40);
///<summary>
///Testuje wydajnoœæ struktur
///</summary>
void test();
///<summary>
///Zapisuje test
///</summary>
void saveTest(const std::string & fileName, double data[TEST_SIZE]);
///<summary>
///Inicjalizuje strukturê na podstawie klasy vector
///</summary>
void initVector(vector<long> & vec);
///<summary>
///Inicjalizuje strukturê na podstawie klasy list
///</summary>
void initList(list<long> & list);

int main()
{
	srand(time(NULL));
	mainMenu();
	return 0;
}

void mainMenu()
{
	system("cls");
	cout << "-----------Program do zarzadzania strukturami danych-----------\n\n"
		<< "Wybierz strukture:\n[1] Tablica\n[2] Lista\n[3] Kopiec\n[4] Drzewo czerwono-czarne\n[5] Drzewo AVL\n[6] Generuj dane\n[7] Wyjscie\n";
	short option = getting<short>();
	while (option < 1 || option > 7)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	if (option == 7)
		return;
	else if (option == 6)
	{
		generateData();
		return mainMenu();
	}
	else if (option == 1 || option == 2)
		return arrayMenu(option);
	return treeMenu(option);
}

void treeMenu(short type, ITree * dataStructure)
{
	system("cls");
	if(dataStructure == nullptr)
		if (!fileExist("data.txt"))
			if (!generateData())
				return;
	switch (type)
	{
	case 3:
	{
		cout << "-----------Kopiec-----------\n\n";
		if (dataStructure == nullptr)
			dataStructure = new Heap("data.txt");
		break;
	}
	case 4:
	{
		cout << "-----------Drzewo czerwono-czarne-----------\n\n";
		if (dataStructure == nullptr)
			dataStructure = new TreeRedBlack("data.txt");
		break;
	}
	case 5:
	{
		cout << "-----------Drzewo AVL-----------\n\n";
		if (dataStructure == nullptr)
			dataStructure = new TreeAVL("data.txt");
		break;
	}
	default:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	}
	cout << "[1] Wyswietl\n[2] Dodaj\n[3] Usun\n[4] Sprawdz czy zawiera liczbe\n[5] Wybierz inna strukture\n[6] Wyjscie\n";
	short option = getting<short>();
	while (option < 1 || option > 10)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	switch (option)
	{
	case 1:
	{
		dataStructure->show();
		break;
	}
	case 2:
	{
		cout << "Podaj liczbe do dodania: ";
		dataStructure->add(getting<long>());
		break;
	}
	case 3:
	{
		cout << "Podaj klucz do usuniecia: ";
		dataStructure->erase(getting<long>());
		break;
	}
	case 4:
	{

		cout << "Podaj liczbe do sprawdzenia: ";
		if (dataStructure->contains(getting<long>()))
			cout << "Podana wartosc znajduje sie w drzewie\n";
		else
			cout << "Podana wartosc nie znajduje sie w drzewie\n";
		break;
	}
	case 5:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return mainMenu();
	}
	case 6:
	default:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	}
	cout << "Czy chcesz wyjsc z programu?\n[1] Tak\n[2] Nie\n";
	option = getting<short>();
	while (option < 1 || option > 2)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	if (option == 1)
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	else
		return treeMenu(type, dataStructure);
}

void arrayMenu(short type, IArray * dataStructure)
{
	system("cls");
	if (dataStructure == nullptr)
		if (!fileExist("data.txt"))
			if (!generateData())
				return;
	switch (type)
	{
	case 1:
	{
		cout << "-----------Tablica-----------\n\n";
		if (dataStructure == nullptr)
			dataStructure = new Table("data.txt");
		break;
	}
	case 2:
	{
		cout << "-----------Lista-----------\n\n";
		if (dataStructure == nullptr)
			dataStructure = new List("data.txt");
		break;
	}
	default:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	}
	cout << "[1] Wyswietl\n[2] Dodaj na poczatku\n[3] Dodaj na koncu\n[4] Dodaj w losowym miejscu\n[5] Usun na poczatku\n[6] Usun na koncu\n"
		<< "[7] Usun w losowym miejscu\n[8] Sprawdz czy zawiera liczbe\n[9] Wybierz inna strukture\n[10] Wyjscie\n";
	short option = getting<short>();
	while (option < 1 || option > 10)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	switch (option)
	{
	case 1:
	{
		dataStructure->show();
		break;
	}
	case 2:
	{
		cout << "Podaj liczbe: ";
		dataStructure->pushFront(getting<long>());
		break;
	}
	case 3:
	{
		cout << "Podaj liczbe: ";
		dataStructure->pushBack(getting<long>());
		break;
	}
	case 4:
	{
		if(type == 1)
			cout << "Podaj indeks i liczbe do wstawienia\nIndeks: ";
		else
			cout << "Podaj liczbe, za ktora wstawic wartosc do listy i liczbe do wstawienia\nWartosc poprzedzajaca: ";
		long arg = getting<long>();
		cout << "Liczba: ";
		dataStructure->insert(arg, getting<long>());
		break;
	}
	case 5:
	{
		dataStructure->popFront();
		break;
	}
	case 6:
	{
		dataStructure->popBack();
		break;
	}
	case 7:
	{
		if (type == 1)
			cout << "Podaj indeks do usuniecia: ";
		else
			cout << "Podaj wartosc do usuniecia: ";
		dataStructure->erase(getting<long>());
		break;
	}
	case 8:
	{
		cout << "Podaj liczbe do sprawdzenia: ";
		if (dataStructure->contains(getting<long>()))
			cout << "Podana wartosc znajduje sie w strukturze\n";
		else
			cout << "Podana wartosc nie znajduje sie w strukturze\n";
		break;
	}
	case 9:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return mainMenu();
	}
	case 10:
	default:
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	}
	cout << "Czy chcesz wyjsc z programu?\n[1] Tak\n[2] Nie\n";
	option = getting<short>();
	while (option < 1 || option > 2)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	if (option == 1)
	{
		if (dataStructure != nullptr)
			delete dataStructure;
		return;
	}
	else
		return arrayMenu(type, dataStructure);
}

bool fileExist(const std::string & fileName)
{
	std::ifstream fin(fileName);
	bool exist = fin.good();
	fin.close();
	return exist;
}

bool generateData(long count)
{
	std::ofstream fout("data.txt");
	if (fout.good())
	{
		fout << count;
		for (long i = 0; i < count; ++i)
			fout << std::endl << (rand() % int(241308 - i * 19.98)) * rand() % int(2413.08 + i * 19.98) * pow(-1, rand());
	}
	else
		return false;
	fout.close();
	return true;
}
#pragma region tests
void test()
{
	LARGE_INTEGER performanceCountStart, performanceCountEnd, frequency;
	QueryPerformanceFrequency(&frequency);
	double time[100]{ 0 };
#pragma region table
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.popBack();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	std::stringstream name;
	name << "table_popBack_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str(""); 
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.popFront();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_popFront_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str(""); 
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.pushFront(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_pushFront_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str(""); 
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.pushBack(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_pushBack_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str(""); 
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.insert(100 + rand() % (TEST_SIZE - 199), 1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_insert_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str(""); 
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.erase(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_erase_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Table data("data.txt");
		performanceCountStart = getTime();
		data.contains(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "table_contains_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region list
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.popBack();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_popBack_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.popFront();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_popFront_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.pushFront(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_pushFront_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.pushBack(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_pushBack_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.insert(100 + rand() % (TEST_SIZE - 199), 1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_insert_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.erase(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_erase_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		List data("data.txt");
		performanceCountStart = getTime();
		data.contains(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "list_contains_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region heap
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Heap data("data.txt");
		performanceCountStart = getTime();
		data.add(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "heap_add_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Heap data("data.txt");
		performanceCountStart = getTime();
		data.erase(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "heap_remove_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		Heap data("data.txt");
		performanceCountStart = getTime();
		data.contains(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "heap_contains_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region treeRB
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeRedBlack data("data.txt");
		performanceCountStart = getTime();
		data.add(TEST_SIZE + 1 + i);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeRB_add_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeRedBlack data("data.txt");
		performanceCountStart = getTime();
		data.erase(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeRB_remove_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeRedBlack data("data.txt");
		performanceCountStart = getTime();
		data.contains(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeRB_contains_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region treeAVL
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeAVL data("data.txt");
		performanceCountStart = getTime();
		data.add(TEST_SIZE + 1 + i);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeAVL_add_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeAVL data("data.txt");
		performanceCountStart = getTime();
		data.erase(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeAVL_remove_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		TreeAVL data("data.txt");
		performanceCountStart = getTime();
		data.contains(100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "treeAVL_contains_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region vectorSTL
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		vector<long> data;
		initVector(data);
		performanceCountStart = getTime();
		data.pop_back();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlVector_pop_back_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		vector<long> data;
		initVector(data);
		performanceCountStart = getTime();
		data.push_back(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlVector_push_back_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		vector<long> data;
		initVector(data);
		performanceCountStart = getTime();
		data.insert(data.begin() + 100 + rand() % (TEST_SIZE - 199), 1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlVector_insert_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		vector<long> data;
		initVector(data);
		performanceCountStart = getTime();
		data.erase(data.begin() + 100 + rand() % (TEST_SIZE - 199));
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlVector_erase_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
#pragma region listSTL
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		performanceCountStart = getTime();
		data.pop_back();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_pop_back_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		performanceCountStart = getTime();
		data.pop_front();
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_pop_front_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		performanceCountStart = getTime();
		data.push_front(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_push_front_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		performanceCountStart = getTime();
		data.push_back(1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_push_back_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		auto iter = data.begin();
		for (long i = 0, size = 100 + rand() % (TEST_SIZE - 199); i < size; ++i)
			++iter;
		performanceCountStart = getTime();
		data.insert(iter, 1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_insert_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
	for (long i = 0; i < 100; ++i)
	{
		generateData(TEST_SIZE);
		list<long> data;
		initList(data);
		auto iter = data.begin();
		for (long i = 0, size = 100 + rand() % (TEST_SIZE - 199); i < size; ++i)
			++iter;
		performanceCountStart = getTime();
		data.insert(iter, 1);
		performanceCountEnd = getTime();
		time[i] = 1000 * (performanceCountEnd.QuadPart - performanceCountStart.QuadPart) / (double)frequency.QuadPart;
	}
	name << "stlList_erase_" << TEST_SIZE << ".txt";
	saveTest(name.str(), time);
	name.str("");
#pragma endregion
}

void saveTest(const std::string & fileName, double data[TEST_SIZE])
{
	std::ofstream fout("TEST/" + fileName);
	if (fout.good())
	{
		fout.precision(std::numeric_limits<double>::max_digits10);
		fout << data[0];
		for (long i = 1; i < 100; ++i)
			fout << std::endl << data[i];
	}
	fout.close();
}

void initVector(vector<long>& vec)
{
	std::ifstream fin("data.txt");
	if (fin.good())
	{
		long count = 0;
		fin >> count;
		if (count)
			for (long i = 0, temp = 0; i < count; i++)
			{
				fin >> temp;
				vec.push_back(temp);
			}
	}
	fin.close();
}

void initList(list<long>& list)
{
	std::ifstream fin("data.txt");
	if (fin.good())
	{
		long count = 0;
		fin >> count;
		if (count)
			for (long i = 0, temp = 0; i < count; i++)
			{
				fin >> temp;
				list.push_back(temp);
			}
	}
	fin.close();
}
#pragma endregion