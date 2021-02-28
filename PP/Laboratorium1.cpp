/*
Autor: Marek Machliñski
Grupa: WTN 7:30
Temat: Laboratorium 1
Data: 3 paŸdziernika 2017 r.
*/

#include <iostream>
#include <cstdio>
#include <cmath>

using namespace std;

int main()
{
	cout << " Autor: Marek Machlinski (WTN 7:30)\n\n";

	cout << "Zadanie 1\na)\nMarek Machlinski\nWroclaw ul. Ciasteczkowa 36\n123 456 789\nmanoman@gmail.com\n";
	printf("b)\nMarek Machlinski\nWroclaw ul. Morelowa 7\n123 456 789\nsystem32@gmail.com\n");

	cout << "Zadanie 2\na)\nPodaj trzy liczby calkowite: ";
	int a, b, c;
	cin >> a >> b >> c;
	cout << "Suma: " << a + b + c << "\nIloczyn: " << a * b * c << "\nSrednia arytmetyczna: " << float(a + b + c) / 3 << endl;
	
	printf("b)\nPodaj trzy liczby calkowite: ");
	scanf("%i %i %i", &a, &b, &c);
	printf("Suma: %i\nIloczyn: %i\nSrednia arytmetyczna: %f\n", a + b + c, a * b * c, float(a + b + c) / 3);
	
	printf("Zadanie 3\na)\nPodaj promien kola: ");
	int r;
	scanf("%i", &r);
	printf("Obwod kola: %f\nPole kola: %f\n", 2 * r * M_PI, pow(r, 2) * M_PI);
	
	cout << "b)\nPodaj promien kola: ";
	cin >> r;
	cout << "Obwod kola: " << 2 * r * M_PI << "\nPole kola: " << pow(r, 2) * M_PI << endl;
}