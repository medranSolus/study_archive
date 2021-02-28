#include "pch.h"

LARGE_INTEGER getTime()
{
	LARGE_INTEGER time;
	DWORD_PTR oldmask = SetThreadAffinityMask(GetCurrentThread(), 0);
	QueryPerformanceCounter(&time);
	SetThreadAffinityMask(GetCurrentThread(), oldmask);
	return time;
}
/*
// BFS - listy s�siedztwa
// Data: 23.07.2013
// (C)2013 mgr Jerzy Wa�aszek
//---------------------------

#include <iostream>
#include <iomanip>

using namespace std;

// Typy dla dynamicznej tablicy list s�siedztwa i kolejki

struct slistEl
{
	slistEl * next;
	int v;
};

// Zmienne globalne

int n;                   // Liczba wierzcho�k�w
slistEl ** A;            // Macierz s�siedztwa
bool * visited;          // Tablica odwiedzin

// Procedura przej�cia wszerz
//---------------------------
void BFS(int v)
{
	slistEl *p, *q, *head, *tail; // Kolejka

	q = new slistEl;        // W kolejce umieszczamy v
	q->next = NULL;
	q->v = v;
	head = tail = q;

	visited[v] = true;      // Wierzcho�ek v oznaczamy jako odwiedzony

	while (head)
	{
		v = head->v;          // Odczytujemy v z kolejki

		q = head;             // Usuwamy z kolejki odczytane v
		head = head->next;
		if (!head) tail = NULL;
		delete q;

		cout << setw(3) << v;

		for (p = A[v]; p; p = p->next)
			if (!visited[p->v])
			{
				q = new slistEl; // W kolejce umieszczamy nieodwiedzonych s�siad�w
				q->next = NULL;
				q->v = p->v;
				if (!tail) head = q;
				else      tail->next = q;
				tail = q;
				visited[p->v] = true; // i oznaczamy ich jako odwiedzonych
			}
	}
}

// **********************
// *** PROGRAM G��WNY ***
// **********************

int main()
{
	int m, i, v1, v2;
	slistEl *p, *r;

	cin >> n >> m;               // Czytamy liczb� wierzcho�k�w i kraw�dzi

	A = new slistEl *[n];       // Tworzymy tablic� list s�siedztwa
	visited = new bool[n];       // Tworzymy tablic� odwiedzin

	// Tablic� wype�niamy pustymi listami

	for (i = 0; i < n; i++)
	{
		A[i] = NULL;
		visited[i] = false;
	}

	// Odczytujemy kolejne definicje kraw�dzi

	for (i = 0; i < m; i++)
	{
		cin >> v1 >> v2;    // Wierzcho�ek startowy i ko�cowy kraw�dzi
		p = new slistEl;    // Tworzymy nowy element
		p->v = v2;          // Numerujemy go jako v2
		p->next = A[v1];    // Dodajemy go na pocz�tek listy A[v1]
		A[v1] = p;
	}

	cout << endl;

	// Przechodzimy graf wszerz

	BFS(0);

	// Usuwamy tablic� list s�siedztwa

	for (i = 0; i < n; i++)
	{
		p = A[i];
		while (p)
		{
			r = p;
			p = p->next;
			delete r;
		}
	}
	delete[] A;
	delete[] visited;

	cout << endl;

	return 0;
}*/