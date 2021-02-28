/*
Autor: Marek Machliñski
Grupa: WTN 7:30
Temat: Laboratorium 2
Data: 10 paŸdziernika 2017 r.
*/

#include <iostream>
#include <cmath>
#include <iomanip>
#include <climits>
#include <ctime>
#include <cstdlib>
using namespace std;

void minMaxRandom();
void minMax();
void chain();
void table(double min, double max, unsigned int verse);
void radiusList();
void checkDate();
inline double delta(double a, double b, double c) { return b * 2 - 4 * a * c; }
void squareFunct();
template<typename T>
T getting();
void menu();

int main()
{
	srand(time(NULL));
	menu();
	return 0;
}

void menu()
{
	cout << " Autor: Marek Machlinski (WTN 7:30)\n\n";
	cout << "Wybierz akcje programu:\n"
		<< "[1] Obliczenie miejsc zerowych funkcji kwadratowej\n"
		<< "[2] Sprawdzenie poprawnosci wpisanej daty\n"
		<< "[3] Obliczenie obwodu i promienia kola dla zadanego przedzialu i ilosci promnieni\n"
		<< "[4] Obliczenie ciagu dla zadanej liczby\n"
		<< "[5] Losowanie najwiekszej i najmniejszej liczby z zadanego przedzialu\n"
		<< "[6] Zakoncz program\n";
	short N = getting<short>();
	while (N < 1 || N>6)
	{
		cout << "Blad, nie ma takiej akcji. Wybierz ponownie.\n";
		N = getting<short>();
	}
	switch (N)
	{
	case 1:
	{
		squareFunct();
		break;
	}
	case 2:
	{
		checkDate();
		break;
	}
	case 3:
	{
		radiusList();
		break;
	}
	case 4:
	{
		chain();
		break;
	}
	case 5:
	{
		cout << "Wybierz wariant:\n1) Samodzielne wybranie przedzialu\n2) Losowy przedzial\n";
		short opt = getting<short>();
		while (opt < 1 || opt > 2)
		{
			cout << "Blad, nie ma takiej opcji. Wybierz ponownie: ";
			opt = getting<short>();
		}
		switch (opt)
		{
		case 1:
		{
			minMax();
			break;
		}
		case 2:
		{
			minMaxRandom();
			break;
		}
		}
		break;
	}
	case 6:
	{
		cout << "Do widzenia.";
		exit(0);
	}
	}
	cout << "\nCzy chcesz wykonac jeszcze jakies dzialanie?\n[1] Tak\n[2] Nie\n";
	N = getting<short>();
	while (N < 1 || N>2)
	{
		cout << "Blad, nie ma takiej akcji. Wybierz ponownie.\n";
		N = getting<short>();
	}
	if (N == 1)
	{
		system("cls");
		menu();
	}
	else
	{
		cout << "Do widzenia.";
		exit(0);
	}
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

void squareFunct()
{
	double a, b, c;
	cout << "Aby obliczyc miejsca zerowe funkcji podaj jej wspolczynniki (a, b, c):\na= ";
	a = getting<double>();
	while (a == 0)
	{
		cout << "Blad, wspolczynnik a nie moze byc rowny 0. Podaj ponownie: ";
		a = getting<double>();
	}
	cout << "b= ";
	b = getting<double>();
	cout << "c= ";
	c = getting<double>();
	double dlt = delta(a, b, c);
	if (dlt == 0)
	{
		cout << "Miejsce zerowe funkcji " << a << "*x^2 + " << b << "*x + " << c << " wynosi:\nx= " << -b / (2 * a);
		return;
	}
	else if (dlt > 0)
	{
		cout << "Miejsca zerowe funkcji " << a << "*x^2 + " << b << "*x + " << c << " wynosza:\nx1= " << (-b - sqrt(dlt)) / (2 * a) << "\nx2= " << (-b + sqrt(dlt)) / (2 * a);
		return;
	}
	else
	{
		cout << "Funkcja " << a << "*x^2 + " << b << "*x + " << c << " nie posiada miejsc zerowych.";
		return;
	}
}

void checkDate()
{
	unsigned short day, month;
	unsigned int year;
	cout << "Prosze wpisac date do sprawdzenia (DD-MM-RRRR):\nDzien= ";
	day = getting<unsigned short>();
	while (day < 1 || day>31)
	{
		cout << "Blad, w miesiacu nie moze byc mniej dni niz 1 oraz wiecej niz 31. Podaj ponownie: ";
		day = getting<unsigned short>();
	}
	cout << "Miesiac= ";
	month = getting<unsigned short>();
	while (month < 1 || month>12)
	{
		cout << "Blad, podaj miesiac z zakresu liczb <1;12>.\nMiesiac= ";
		month = getting<unsigned short>();
	}
	cout << "Rok= ";
	year = getting<unsigned int>();
	while (year < 1)
	{
		cout << "Blad, numerowanie rokow zaczyna sie od roku pierwszego. Podaj ponownie: ";
		year = getting<unsigned int>();
	}
	if (day > 30 && month == (4 || 6 || 9 || 11))
	{
		cout << "Data " << day << "-" << month << "-" << year << " jest niepoprawna. ";
		switch (month)
		{
		case 4:
		{
			cout << "W kwietniu ";
			break;
		}
		case 6:
		{
			cout << "W czerwcu ";
			break;
		}
		case 9:
		{
			cout << "We wrzesniu ";
			break;
		}
		case 11:
		{
			cout << "W listopadzie ";
			break;
		}
		}
		cout << "nie moze byc wiecej dni niz 30.";
		return;
	}
	else if (month == 2)
	{
		if (day > 29)
		{
			cout << "Data " << day << "-" << month << "-" << year << " jest niepoprawna. W lutym nie moze byc wiecej dni niz 29.";
			return;
		}
		bool isCrimeYear = true;
		if (year % 4 != 0)
			isCrimeYear = false;
		else if (year % 100 == 0 && year % 400 != 0)
			isCrimeYear = false;
		if (!isCrimeYear&&day == 29)
		{
			cout << "Data " << day << "-" << month << "-" << year << " jest niepoprawna. Rok " << year << " jest rokiem nieprzestepnym, wiec luty ma tylko 28 dni.";
			return;
		}
	}
	else
	{
		cout << "Data " << day << "-" << month << "-" << year << " jest poprawna.";
		return;
	}
}

void radiusList()
{
	cout << "Podaj minimalny i maksymalny zakres promienia kola:\nMinimalne r= ";
	double min, max;
	min = getting<double>();
	while (min <= 0)
	{
		cout << "Blad, promien nie moze byc mniejszy badz rowny 0. Podaj ponownie: ";
		min = getting<double>();
	}
	cout << "Maksymalne r= ";
	max = getting<double>();
	while (max <= min)
	{
		cout << "Blad, maksymalny promien nie moze byc mniejszy badz rowny minimalnemu promieniowi. Podaj ponownie: ";
		max = getting<double>();
	}
	cout << "Podaj liczbe wierszy do wyswietlenia: ";
	unsigned int verse = getting<unsigned int>();
	while (verse < 1)
	{
		cout << "Blad, liczba wierszy nie moze byc mniejsza niz 1. Podaj ponownie: ";
		verse = getting<unsigned int>();
	}
	table(min, max, verse);
	return;
}

void table(double min, double max, unsigned int verse)
{
	cout << setfill('=') << setw(81) << '=' << setfill(' ') << "\n|" << setw(10) << "Lp" << setw(10) << '|' << setw(10) << "Promien" << setw(10) << '|' << setw(10) << "Obwod Kola" << setw(10) << '|' << setw(10) << "Pole Kola" << setw(10) << '|' << endl << setfill('=') << setw(81) << '=';
	double rChange = (max - min ) / (verse - 1);
	for (int i = 1; i <= verse; ++i, min += rChange)
		cout << "\n|" << setfill(' ') << setw(10) << i << setw(10) << '|' << setw(10) << fixed << setprecision(2) << min << setprecision(5) << setw(10) << '|' << setw(10) << 2 * min*M_PI << setw(10) << '|' << setw(10) << M_PI*pow(min, 2) << setw(10) << '|';
	cout << '\n' << setfill('=') << setw(81) << '=';
}

void chain()
{
	cout << "Podaj liczbe wymagana do stworzenia ciagu (liczba calkowita dodatnia): ";
	unsigned int n = getting<unsigned int>();
	while (n < 1)
	{
		cout << "Blad, ilosc musi byc wieksza od 1. Podaj ponownie: ";
		n = getting<unsigned int>();
	}
	for (int i = 1; n != 1; ++i)
	{
		cout << i << ", " << n;
		if (n % 2 == 0)
		{
			cout << ", parzyste, ";
			n /= 2;
		}
		else
		{
			cout << ", nieparzyste, ";
			n = 3 * n + 1;
		}
		cout << n << endl;
	}
	return;
}

void minMax()
{
	int min, max, sum = 0, Maxi = INT_MIN, Mini = INT_MAX;
	cout << "Podaj przedzial losowania:\nMinimum= ";
	min = getting<int>();
	cout << "Maximum= ";
	max = getting<int>();
	while (max<=min)
	{
		cout << "Maxymalna wartosc losowaniu musi byc wieksza od minimalnej. Podaj ponownie: ";
		max = getting<int>();
	}
	for (int i = 0, tmp; i < 20; ++i)
	{
		tmp = min + rand() % (max - min + 1);
		cout << tmp << endl;
		sum += tmp;
		if (tmp > Maxi)
			Maxi = tmp;
		if (tmp < Mini)
			Mini = tmp;
	}
	cout << "Najwieksza wylosowana liczba to " << Maxi << ", a najmniejsza to " << Mini << "\nSrednia wylosowanych liczb: " << float(sum) / 20;
	return;
}

void minMaxRandom()
{
	float min, max, sum = 0, Maxi = FLT_MIN, Mini = FLT_MAX;
	min = rand();
	max = min + rand();
	while(max==min)
		max = min + rand();
	for (int i = 0; i < 20; ++i)
	{
		float tmp;
		tmp = min + (max - min) * rand() / float(RAND_MAX);
		cout << tmp << endl;
		sum += tmp;
		if (tmp > Maxi)
			Maxi = tmp;
		if (tmp < Mini)
			Mini = tmp;
	}
	cout << fixed << setprecision(2) << "Najwieksza wylosowana liczba to " << Maxi << ", a najmniejsza to " << Mini << "\nSrednia wylosowanych liczb: " << sum / 20;
	return;
}