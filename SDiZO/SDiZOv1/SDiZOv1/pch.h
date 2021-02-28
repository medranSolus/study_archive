#pragma once
#include <iostream>
#include <Windows.h>
#undef max

///<summary>
///Pobiera czas procesora
///</summary>
LARGE_INTEGER getTime();

///<summary>
///Wczytuje dowolne dane
///</summary>
template<typename T>
T getting()
{
	T a;
	std::cin >> a;
	while (std::cin.fail())
	{
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
		std::cout << "Blad, podaj ponownie: ";
		std::cin >> a;
	}
	return a;
}