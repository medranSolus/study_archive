#include "pch.h"
#include "GraphList.h"
#include "GraphMatrix.h"
#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib>
#include <ctime>
using std::cout;
using std::string;

GraphMatrix * graphMatrix = nullptr;
GraphList * graphList = nullptr;

void saveTest(double *list, double *matrix, long long size, short density);
void runTest(long long size, short density);
void test();
void loadData();
void generateData(long long size, short density);
bool fileExist(const string & fileName);
void runAlgorithm(short mask);
void algorithmMenu(short version, const string & banner, const string & option1, const string & option2);
void mainMenu();

int main()
{
	srand(time(NULL));
	if (!fileExist("data.txt"))
		generateData(GRAPHSIZE, DENSITY);
	else
		loadData();
	mainMenu();
	if (graphMatrix != nullptr)
		delete graphMatrix;
	if (graphList != nullptr)
		delete graphList;
	return 0;
}

void mainMenu()
{
	system("cls");
	cout << "-----------Program do operacji na grafach-----------\n\n"
		<< "[1] Wyznaczanie minimalnego drzewa rozpinajacego\n[2] Znajdowanie najkrotszej sciezki w grafie\n[3] Wyznaczanie maksymalnego przeplywu\n"
		<< "[4] Generacja nowych danych\n[5] Wyjscie\n";
	short option = getting<short>();
	while (option < 1 || option > 5)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	switch (option)
	{
	case 1:
		return algorithmMenu(MST, "Minimalne drzewo rozpinajace", "Algorytm Prima", "Algorytm Kruskala");
	case 2:
		return algorithmMenu(PATH, "Najkrotsza sciezka w grafie", "Algorytm Dijkstry", "Algorytm Forda-Bellmana");
	case 3:
		return algorithmMenu(FLOW, "Maksymalny przeplyw, algorytm Forda-Fulkersona", "Z przeszukiwaniem w glab", "Z przeszukiwaniem w szerz");
	case 4:
	{
		cout << "Podaj iloœæ wierzcholkow w grafie: ";
		long long size = getting<long long>();
		while (!size)
		{
			cout << "Graf nie moze miec 0 wierzcholkow, podaj ponownie: ";
			size = getting<long long>();
		}
		cout << "Podaj gestosc grafu (25|50|75|99%): ";
		short density = getting<short>();
		while (density != 25 && density != 50 && density != 75 && density != 99)
		{
			cout << "Dostepne gestosci to 25|50|75|99%, podaj ponownie: ";
			density = getting<short>();
		}
		generateData(size, density);
		return mainMenu();
	}
	case 5:
	default:
		return;
	}
}

void algorithmMenu(short version, const string & banner, const string & option1, const string & option2)
{
	system("cls");
	cout << "-----------" + banner + "-----------\n\n"
		<< "[1] " + option1 + "\n[2] " + option2 + "\n[3] Wyswietlenie grafu\n[4] Powrot\n[5] Wyjscie\n";
	short option = getting<short>();
	while (option < 1 || option > 5)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	switch (option)
	{
	case 1:
	{
		runAlgorithm(version);
		break;
	}
	case 2:
	{
		runAlgorithm(version + 1);
		break;
	}
	case 3:
	{
		cout << "\nGraf w postaci macierzy incydencji:\n";
		graphMatrix->show();
		cout << "\nGraf w postaci listy sasiedztwa:\n";
		graphList->show();
		cout << std::endl;
		break;
	}
	case 4:
		return mainMenu();
	case 5:
	default:
		return;
	}
	cout << "Czy chcesz wyjsc z programu?\n[1] Tak\n[2] Nie\n";
	option = getting<short>();
	while (option < 1 || option > 2)
	{
		cout << "Nie ma takiej opcji, wybierz ponownie: ";
		option = getting<short>();
	}
	if (option == 2)
		return algorithmMenu(version, banner, option1, option2);
	return;
}

void runAlgorithm(short mask)
{
	if (mask & FLOW)
	{
		cout << "Podaj wierzcholek poczatkowy: ";
		long long source = getting<long long>();
		while (source < 0 || source >= graphMatrix->vertexCount())
		{
			cout << "Blad, wybierz wierzcholek z przedzialu <0;" << graphMatrix->vertexCount() - 1 << ">: ";
			source = getting<long long>();
		}
		cout << "Podaj wierzcholek koncowy: ";
		long long target = getting<long long>();
		while (target < 0 || target >= graphMatrix->vertexCount() || target == source)
		{
			cout << "Blad, wybierz wierzcholek z przedzialu <0;" << graphMatrix->vertexCount() - 1 << "> rozny od startowego wierzcholka: ";
			target = getting<long long>();
		}
		FlowMatrixes * matrix = nullptr;
		FlowList * list = nullptr;
		if (mask & 1)
		{
			matrix = graphMatrix->maximalFlowBFS(source, target);
			list = graphList->maximalFlowBFS(source, target);
		}
		else
		{
			matrix = graphMatrix->maximalFlowDFS(source, target);
			list = graphList->maximalFlowDFS(source, target);
		}
		cout << "\nGraf w postaci macierzy incydencji:\n";
		if (matrix->connection)
		{
			cout << "\nMaksymalny przeplyw = " << matrix->value << std::endl;
			for (long long i = 0; i < matrix->size; ++i)
				for (long long j = 0; j < matrix->size; ++j)
					if (matrix->bandwidthMatrix[i][j] && matrix->flowNettoMatrix[i][j] && j != source && i != target)
					{
						cout << "  ";
						if (i == source)
							cout << '[' << i << ']';
						else
							cout << ' ' << i << ' ';
						cout << " --<" << matrix->bandwidthMatrix[i][j] << ">--> ";
						if (j == target)
							cout << '[' << j << "]\n";
						else
							cout << j << std::endl;
					}
		}
		else
			cout << "\nNie znaleziono polaczenia.\n";
		cout << std::endl;
		for (long long i = 0; i < matrix->size; i++)
		{
			delete[] matrix->bandwidthMatrix[i];
			delete[] matrix->flowNettoMatrix[i];
		}
		delete[] matrix->bandwidthMatrix;
		delete[] matrix->flowNettoMatrix;
		delete matrix;
		cout << "\nGraf w postaci listy sasiedztwa:\n";
		if (list->connection)
		{
			cout << "\nMaksymalny przeplyw = " << list->value << std::endl;
			for (long long i = 0; i < list->size; ++i)
				for (Edge * z = list->list[i]; z; z = z->getNext())
					if (z->getValue() && z->getFlow() && z->getNextVertex() != source && i != target)
					{
						cout << "  ";
						if (i == source)
							cout << '[' << i << ']';
						else
							cout << ' ' << i << ' ';
						cout << " --<" << z->getValue() << ">--> ";
						if (z->getNextVertex() == target)
							cout << '[' << z->getNextVertex() << "]\n";
						else
							cout << z->getNextVertex() << std::endl;
					}
		}
		else
			cout << "\nNie znaleziono polaczenia.\n";
		cout << std::endl;
		Edge * temp = nullptr, *next = nullptr;
		for (long long i = 0; i < list->size; ++i)
		{
			temp = list->list[i];
			while (temp)
			{
				next = temp->getNext();
				delete temp;
				temp = next;
			}
		}
		delete[] list;
	}
	else if (mask & PATH)
	{
		cout << "Podaj wierzcholek poczatkowy: ";
		long long first = getting<long long>();
		while (first < 0 || first >= graphMatrix->vertexCount())
		{
			cout << "Blad, wybierz wierzcholek z przedzialu <0;" << graphMatrix->vertexCount() - 1 << ">: ";
			first = getting<long long>();
		}
		cout << "Podaj wierzcholek koncowy: ";
		long long last = getting<long long>();
		while (last < 0 || last >= graphMatrix->vertexCount())
		{
			cout << "Blad, wybierz wierzcholek z przedzialu <0;" << graphMatrix->vertexCount() - 1 << ">: ";
			last = getting<long long>();
		}
		Path * list = nullptr, * matrix = nullptr;
		if (mask & 1)
		{
			matrix = graphMatrix->findPathFordBellman(first, last);
			list = graphList->findPathFordBellman(first, last);
		}
		else
		{
			matrix = graphMatrix->findPathDijkstra(first, last);
			list = graphList->findPathDijkstra(first, last);
		}
		cout << "\nGraf w postaci macierzy incydencji:\n";
		if (abs(matrix->value) == MAXLONGLONG || matrix->value < 0)
			cout << "Nie znaleziono polaczenia.\n";
		else
		{
			cout << "Droga: " << matrix->tab[matrix->count - 1];
			for (long long i = matrix->count - 2; i >= 0; --i)
			{
				cout << " - " << matrix->tab[i];
			}
			cout << "\nKoszt: " << matrix->value << std::endl;
		}
		free(matrix->tab);
		delete matrix;
		cout << "\nGraf w postaci listy sasiedztwa:\n";
		if (abs(list->value) == MAXLONGLONG || list->value < 0)
			cout << "Nie znaleziono polaczenia.\n";
		else
		{
			cout << "Droga: " << list->tab[list->count - 1];
			for (long long i = list->count - 2; i >= 0; --i)
			{
				cout << " - " << list->tab[i];
			}
			cout << "\nKoszt: " << list->value << "\n\n";
		}
		free(list->tab);
		delete list;
	}
	else
	{
		MinimalSpanTree * list = nullptr, * matrix = nullptr;
		if (mask & 1)
		{
			list = graphList->minimalTreeKruskal();
			matrix = graphMatrix->minimalTreeKruskal();
		}
		else
		{
			list = graphList->minimalTreePrim();
			matrix = graphMatrix->minimalTreePrim();
		}
		cout << "\nGraf w postaci macierzy incydencji:\n";
		matrix->show();
		cout << "\nGraf w postaci listy sasiedztwa:\n";
		list->show();
		cout << std::endl;
		delete matrix;
		delete list;
	}
}

bool fileExist(const std::string & fileName)
{
	std::ifstream fin(fileName);
	bool exist = fin.good();
	fin.close();
	return exist;
}

void generateData(long long size, short density)
{
	long long edges = size * (size - 1) * density / 100;
	if (graphMatrix == nullptr)
		graphMatrix = new GraphMatrix(size, edges);
	else
		graphMatrix->set(size, edges);
	if (graphList == nullptr)
		graphList = new GraphList(size, edges);
	else
		graphList->set(size, edges);
	std::ofstream fout("data.txt");
	graphMatrix->save(fout);
	fout.close();
}

void loadData()
{
	std::ifstream fin("data.txt");
	graphMatrix = new GraphMatrix(fin);
	fin.close();
	fin.open("data.txt");
	graphList = new GraphList(fin);
	fin.close();
}

#pragma region tests
void test()
{
	runTest(10, 25);
	runTest(10, 50);
	runTest(10, 75);
	runTest(10, 99);
	runTest(25, 25);
	runTest(25, 50);
	runTest(25, 75);
	runTest(25, 99);
	runTest(50, 25);
	runTest(50, 50);
	runTest(50, 75);
	runTest(50, 99);
	runTest(100, 25);
	runTest(100, 50);
	runTest(100, 75);
	runTest(100, 99);
	runTest(200, 25);
	runTest(200, 50);
	runTest(200, 75);
	runTest(200, 99);
}

void runTest(long long size, short density)
{
	LARGE_INTEGER start, end, frequency;
	QueryPerformanceFrequency(&frequency);
	double *list = new double[6], *matrix = new double[6];
	for (size_t i = 0; i < 6; ++i)
		list[i] = matrix[i] = 0.0;
	MinimalSpanTree * tree = nullptr;
	Path * path = nullptr;
	FlowList * flowList = nullptr;
	Edge * temp = nullptr, *next = nullptr;
	FlowMatrixes * flowMatrix = nullptr;
	long long first = 0, last = 0;
	std::cout << '[' << size << '|' << density << "]: 0";
	for (size_t i = 0; i < 100; ++i)
	{
		generateData(size, density);
		#pragma region tree
		start = getTime();
		tree = graphList->minimalTreePrim();
		end = getTime();
		list[0] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		delete tree;
		start = getTime();
		tree = graphMatrix->minimalTreePrim();
		end = getTime();
		matrix[0] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		delete tree;
		start = getTime();
		tree = graphList->minimalTreeKruskal();
		end = getTime();
		list[1] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		delete tree;
		start = getTime();
		tree = graphMatrix->minimalTreeKruskal();
		end = getTime();
		matrix[1] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		delete tree;
		#pragma endregion
		#pragma region path
		first = rand() % size;
		do
		{
			last = rand() % size;
		} while (first == last);
		start = getTime();
		path = graphList->findPathDijkstra(first, last);
		end = getTime();
		list[2] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		free(path->tab);
		delete path;
		start = getTime();
		path = graphMatrix->findPathDijkstra(first, last);
		end = getTime();
		matrix[2] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		free(path->tab);
		delete path;
		start = getTime();
		path = graphList->findPathFordBellman(first, last);
		end = getTime();
		list[3] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		free(path->tab);
		delete path;
		start = getTime();
		path = graphMatrix->findPathFordBellman(first, last);
		end = getTime();
		matrix[3] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		free(path->tab);
		delete path;
		#pragma endregion
		#pragma region flow
		start = getTime();
		flowList = graphList->maximalFlowBFS(first, last);
		end = getTime();
		list[4] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		for (long long i = 0; i < flowList->size; ++i)
		{
			temp = flowList->list[i];
			while (temp)
			{
				next = temp->getNext();
				delete temp;
				temp = next;
			}
		}
		delete[] flowList;
		start = getTime();
		flowMatrix = graphMatrix->maximalFlowBFS(first, last);
		end = getTime();
		matrix[4] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		for (long long i = 0; i < flowMatrix->size; i++)
		{
			delete[] flowMatrix->bandwidthMatrix[i];
			delete[] flowMatrix->flowNettoMatrix[i];
		}
		delete[] flowMatrix->bandwidthMatrix;
		delete[] flowMatrix->flowNettoMatrix;
		delete flowMatrix;
		start = getTime();
		flowList = graphList->maximalFlowDFS(first, last);
		end = getTime();
		list[5] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		for (long long i = 0; i < flowList->size; ++i)
		{
			temp = flowList->list[i];
			while (temp)
			{
				next = temp->getNext();
				delete temp;
				temp = next;
			}
		}
		delete[] flowList;
		start = getTime();
		flowMatrix = graphMatrix->maximalFlowDFS(first, last);
		end = getTime();
		matrix[5] += 1000 * (end.QuadPart - start.QuadPart) / (double)frequency.QuadPart;
		for (long long i = 0; i < flowMatrix->size; i++)
		{
			delete[] flowMatrix->bandwidthMatrix[i];
			delete[] flowMatrix->flowNettoMatrix[i];
		}
		delete[] flowMatrix->bandwidthMatrix;
		delete[] flowMatrix->flowNettoMatrix;
		delete flowMatrix;
		#pragma endregion
		system("cls");
		std::cout << '[' << size << '|' << density << "]: " << i + 1;
	}
	for (size_t i = 0; i < 6; ++i)
	{
		list[i] /= 100.0;
		matrix[i] /= 100.0;
	}
	saveTest(list, matrix, size, density);
	delete[] list;
	delete[] matrix;
}

void saveTest(double *list, double *matrix, long long size, short density)
{
	std::string path = "TEST/" + std::to_string(size), file = "_den_" + std::to_string(density);
	file += ".txt";
	std::ofstream foutList(path + "/list" + file);
	std::ofstream foutMatrix(path + "/matrix" + file);
	for (size_t i = 0; i < 6; ++i)
	{
		foutList << list[i] << ' ';
		foutMatrix << matrix[i] << ' ';
	}
	foutList.close();
	foutMatrix.close();
}
#pragma endregion