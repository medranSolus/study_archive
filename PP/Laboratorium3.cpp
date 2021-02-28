/*
Autor: Marek Machliñski
Grupa: WTN 7:30
Temat: Laboratorium 3
Data: 10 paŸdziernika 2017 r.
*/

#include <iostream>
#include <limits>
#include <cstdlib>
#include <iomanip>
#include <cctype>
#include <conio.h>
using namespace std;

void charCount();
void numberSum();
void nwdNww();
void table();
void tree();
template <typename T>
T getting();
void menu();

int main()
{
	cout << "Autor: Marek Machlinski\n\n";
	menu();
	return 0;
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

void menu()
{
	cout << "[1] Rysowanie choinki\n[2] Wyswietlenie tabliczki mnozenia\n[3] Znalezienie najwiekszego wspolnego dzielnika i najmniejszej wspolnej wielokrotnosci zadanych 2 liczb\n[4] Obliczenie sumy cyfr zadanej liczby\n[5] Wczytywanie znakow i ich zliczanie\n[6] Wyjscie\n";
	int N = getting<int>();
	while (N < 1 || N > 6)
	{
		cout << "Blad, nie ma takiej akcji. Podaj ponownie: ";
		N = getting<int>();
	}
	switch(N)
	{
	case 1:
	{
		tree();
		break;
	}
	case 2:
	{
		table();
		break;
	}
	case 3:
	{
		nwdNww();
		break;
	}
	case 4:
	{
		cout << "Podaj dowolna duza liczbe: ";
		numberSum();
		break;
	}
	case 5:
	{
		charCount();
		break;
	}
	case 6:
		exit(0);
	}
	cout << "Czy chcesz wykonac jeszcze jakas akcje?\n[1] Tak\n[2] Nie\n";
	N = getting<int>();
	while (N < 1 || N > 2)
	{
		cout << "Blad, nie ma takiej opcji. Podaj ponownie: ";
		N = getting<int>();
	}
	if (N == 1)
	{
		system("cls");
		menu();
	}
	else
		exit(0);
}

void tree()
{
	cout << "Podaj wielkosc choinki: ";
	int n = getting<int>();
	for (int I = 1; I <= n; ++I)
	{
		for (int i = n - I; i > 0; --i)
			cout << ' ';
		for (int i = 2 * I - 1; i > 0; --i)
			cout << '*';
		cout << endl;
	}
	for (int i = 1; i < n; ++i)
		cout << ' ';
	cout << "#\n";
}

void table()
{
	cout << "Podaj liczbe kolumn tabliczki: ";
	int col = getting<int>();
	while (col < 1)
	{
		cout << "Blad, liczba kolumn nie moze byc mniejsza od 1. Podaj ponownie: ";
		col = getting<int>();
	}
	cout << "Podaj liczbe wierszy: ";
	int verse = getting<int>();
	while (verse < 1)
	{
		cout << "Blad, liczba wierszy nie moze byc mniejsza od 1. Podaj ponownie: ";
		verse = getting<int>();
	}
	cout << setfill(' ') << setw(5) << '|';
	for (int i = 0; i < col;)
		cout << setw(5) << ++i;
	cout << endl << setfill('-') << setw(5) << '|' << setw(5 * col) << '-' << endl << setfill(' ');
	for (int i = 0; i < verse;)
	{
		cout << setw(3) << ++i << setw(2) << '|';
		for (int j = 1; j <= col; ++j)
			cout << setw(5) << i*j;
		cout << endl;
	}
}

void nwdNww()
{
	cout << "Podaj dwie liczby:\n1= ";
	int A = getting<int>();
	cout << "2= ";
	int B = getting<int>();
	if (B > A)
		swap(B, A);
	int a = A;
	for (int d, b = B; b != 0;)
	{
		d = b;
		b = a%b;
		a = d;
	}
	cout << "Najwiekszy wspolny dzielnik " << A << " i " << B << " to: " << a << ", a najmniejsza wspolna wielokrotnosc to: " << A*B / a << endl;
}

void numberSum()
{
	bool isAgain = false;
	char num;
	int sum = 0;
	for (; cin.peek() != 10;)
	{
		cin >> num;
		if (isdigit(num))
			sum += num - '0';
		else
		{
			cout << "Jeden ze znakow nie jest cyfra. Wprowadz ponownie: ";
			isAgain = true;
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			numberSum();
			break;
		}
	}
	if(!isAgain)
		cout << "Suma cyfr podanej liczby wynosi: " << sum << endl;
}

void charCount()
{
	cout << "Podaj ciag znakow:\n";
	int sumA, sumE, sumI, sumO, sumU, sumY, sum;
	char sign;
	sumA = sumE = sumI = sumO = sumU = sumY = sum = 0;
	do
	{
		sign = getch();
		cout << sign;
		++sum;
		switch (sign)
		{
		case 'A':
		case 'a':
		{
			++sumA;
			break;
		}
		case 'E':
		case 'e':
		{
			++sumE;
			break;
		}
		case 'I':
		case 'i':
		{
			++sumI;
			break;
		}
		case 'O':
		case 'o':
		{
			++sumO;
			break;
		}
		case 'U':
		case 'u':
		{
			++sumU;
			break;
		}
		case 'Y':
		case 'y':
		{
			++sumY;
			break;
		}
		default:
			break;
		}
	} while (sign != 'k');
	cout << "\n\nWpisano ogolem " << sum << " znakow, w tym:\n";
	sum = 0;
	int change;
	for (char tab[] = { 'A', 'E', 'I', 'O', 'U', 'Y' }; ; ++sum)
	{
		cout << "Litera " << tab[sum] << ": ";
		switch (tab[sum])
		{
		case 'A':
		{
			cout << sumA;
			change = sumA;
			break;
		}
		case 'E':
		{
			cout << sumE;
			change = sumE;
			break;
		}
		case 'I':
		{
			cout << sumI;
			change = sumI;
			break;
		}
		case 'O':
		{
			cout << sumO;
			change = sumO;
			break;
		}
		case 'U':
		{
			cout << sumU;
			change = sumU;
			break;
		}
		case 'Y':
		{
			cout << sumY;
			change = sumY;
			break;
		}
		}
		cout << ' ';
		for (; change; --change)
			cout << '#';
		cout << endl;
		if (tab[sum] == 'Y')
			break;
	}
}
