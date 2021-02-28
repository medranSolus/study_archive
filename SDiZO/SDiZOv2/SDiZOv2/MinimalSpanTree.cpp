#include "pch.h"
#include "MinimalSpanTree.h"
#include <iostream>

MinimalSpanTree::MinimalSpanTree(long long size) : count(size)
{
	list = new Node*[count];
	for (long long i = 0; i < count; i++) 
		list[i] = nullptr;
}

Node * MinimalSpanTree::getNeighbours(long long vertex)
{
	if (vertex < count)
		return list[vertex];
	return nullptr;
}

void MinimalSpanTree::addEdge(Edge edge)
{
	weight += edge.getValue();
	list[edge.getStartVertex()] = new Node(edge.getNextVertex(), edge.getValue(), list[edge.getStartVertex()]);
	list[edge.getNextVertex()] = new Node(edge.getStartVertex(), edge.getValue(), list[edge.getNextVertex()]);
}

void MinimalSpanTree::show()
{
	Node * temp = nullptr;
	for (long long i = 0; i < count; ++i)
	{
		std::cout << "  " << i;
		if (list[i])
		{
			std::cout << ": -$" << list[i]->getWeight() << "->" << list[i]->getNextVertex();
			temp = list[i]->getNext();
			while (temp)
			{
				std::cout << ", -$" << temp->getWeight() << "->" << temp->getNextVertex();
				temp = temp->getNext();
			}
			std::cout << std::endl;
		}
		else
			std::cout << ".\n";
	}
	std::cout << "Waga minimalnego drzewa rozpinajacego = " << weight << "\n";
}

MinimalSpanTree::~MinimalSpanTree()
{
	if (list)
	{
		Node * temp, *toDelete;
		for (long long i = 0; i < count; ++i)
		{
			temp = list[i];
			while (temp)
			{
				toDelete = temp;
				temp = temp->getNext();
				delete toDelete;
			}
		}
		delete[] list;
	}
}