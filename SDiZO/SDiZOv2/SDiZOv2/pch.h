#pragma once
#include "Edge.h"
#include <Windows.h>
#undef max

constexpr short GRAPHSIZE = 10;
constexpr short DENSITY = 25;
constexpr short MST = 0;
constexpr short PATH = 2;
constexpr short FLOW = 4;

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

struct Path
{
	long long * tab = nullptr;
	long long count = 0;
	long long value = MAXLONGLONG;
};

struct FlowMatrixes
{
	long long ** bandwidthMatrix = nullptr;
	long long ** flowNettoMatrix = nullptr;
	long long size = 0;
	long long value = 0;
	bool connection = false;
};

struct FlowList
{
	Edge ** list = nullptr;
	long long size = 0;
	long long value = 0;
	bool connection = false;
};