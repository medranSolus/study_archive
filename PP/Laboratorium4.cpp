/*
Autor: Marek Machliñski
Grupa: WTN 7:30
Temat: Laboratorium 4
Data: 7 listopada 2017 r.
*/

#include <iostream>
#include <limits>
#include <cstdlib>
#include <ctime>
#include <iomanip>
#define ROZMIAR 5
using namespace std;

float Tab[ROZMIAR]={0};

void sortTable();
void checkTable();
void showTable();
void getRandomTable();
void getTable();
template<typename T>
T getting();
void menu();

int main()
{
	cout << "Marek Machliñski\n\n";
	srand(time(NULL));
	menu();
	return 0;
}

void menu()
{
	cout << "Wybierz akcje:\n[1] Uzupelnij tablice\n[2] Wypelnij tablice liczbami losowymi\n[3] Posortuj tablice\n[4] Sprawdz zawartosc tablicy\n[5] Wyswietl tablice\n[6] Wyjscie\n";
	short N=getting<short>();
	while(N<1||N>6)
	{
		cout << "Blad, nie ma takiej opcji. Podaj ponownie: ";
		N = getting<short>();
	}
	switch(N)
	{
		case 1:
		{
			getTable();
			break;
		}
		case 2:
		{
			getRandomTable();
			break;
		}
		case 3:
		{
			sortTable();
			break;
		}
		case 4:
		{
			checkTable();
			break;
		}
		case 5:
		{
			showTable();
			break;
		}
		case 6:
			exit(0);
	}
	cout<<"Czy chcesz wykonac jeszcze jakas akcje?\n[1] Tak\n[2] Nie\n";
	N=getting<short>();
	while (N<1||N>2)
	{
		cout << "Blad, nie ma takiej opcji. Podaj ponownie: ";
		N = getting<short>();
	}
	if(N==1)
	{
		system("cls");
		menu();
	}
	else
		exit(0);
}

template<typename T>
T getting()
{
	T a;
	cin >> a;
	bool prblm = cin.fail();
	cin.clear();
	cin.ignore(numeric_limits<streamsize>::max(), '\n');
	while (prblm)
	{
		cout << "Blad, podaj ponownie: ";
		cin >> a;
		prblm = cin.fail();
		cin.clear();
		cin.ignore(numeric_limits<streamsize>::max(), '\n');
	}
	return a;
}

void getTable()
{
	
	cout<<"Podaj "<<ROZMIAR<<" licz rzeczywistych:";
	for(int i=0; i<ROZMIAR;++i)
	{
		cout<<"\nTab["<<i<<"]=";
		Tab[i]=getting<float>();
	}
}

void getRandomTable()
{
	int min, max;
	cout<<"Podaj przedzial losowania <min;max)\nMin=";
	min=getting<int>();
	cout<<"Max=";
	max=getting<int>();
	for(int i=0;i<ROZMIAR;++i)
		Tab[i]=min+(max-min)*rand()/(float)RAND_MAX;
}

void showTable()
{
	cout<<"Tab[  "<<fixed<<setprecision(1);
	for(int i=0;i<ROZMIAR;++i)
		cout<<Tab[i]<<"  ";
	cout<<"]\n";
}

void checkTable()
{
	int  plus, minus, sumPlus, sumMinus;
	plus=minus=sumPlus=sumMinus=0;
	bool grow=true, decrease=true;
	for(int i=0;i<ROZMIAR;++i)
	{
		if(Tab[i]<0)
		{
			++minus;
			sumMinus+=Tab[i];
		}
		else if(Tab[i]>0)
		{
			++plus;
			sumPlus+=Tab[i];
		}
		if(grow&&i>0)
			grow=(Tab[i]>Tab[i-1]?true:false);
		else if(decrease&&i>0)
			decrease=(Tab[i]<Tab[i-1]?true:false);
	}
	cout<<"W tablicy jest "<<plus<<" liczb dodatnich i "<<minus<<" liczb ujemnych\nSuma liczb dodatnich wynosi "<<sumPlus<<", a ujemnych "<<sumMinus<<"\nSrednia arytmetyczna liczb dodatnich wynosi ";
	if(plus==0)
		cout<<0;
	else
		cout<<sumPlus/plus;
	cout<<", a ujemnych ";
	if(minus==0)
		cout<<0;
	else
		cout<<sumMinus/minus;
	cout<<endl;
	if(grow)
		cout<<"Liczby w tablicy sa uporzadkowane rosnaco\n";
	else if(decrease)
		cout<<"Liczby w tablicy sa uporzadkowane malejaco\n";
	else
		cout<<"Liczby w tablicy nie sa uporzadkowane, ani malejaco, ani rosnaco\n";
}

void sortTable()
{
	bool sorted;
	for (int i = 0; i < ROZMIAR; ++i)
	{
		sorted = true;
		for (int j = 0; j < ROZMIAR - 1; ++j)
		{
			if (Tab[j] > Tab[j + 1])
			{
				swap(Tab[j], Tab[j + 1]);
				sorted = false;
			}
		}
	if (sorted) 
		break;
	}
}
	