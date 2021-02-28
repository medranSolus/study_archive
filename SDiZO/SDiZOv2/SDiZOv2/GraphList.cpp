#include "pch.h"
#include "GraphList.h"
#include "DisjunctiveSet.h"
#include "Queue.h"
#include "List.h"
#include <iostream>

Edge * GraphList::getEdge(long long first, long long last) const
{
	if (first < vertexes)
	{
		if (list[first])
		{
			Edge * temp = list[first];
			do
			{
				if (temp->getNextVertex() == last)
					return temp;
				temp = temp->getNext();
			} while (temp != nullptr);
		}
	}
	return nullptr;
}

GraphList::GraphList(std::ifstream & fin)
{
	fin >> edges >> vertexes;
	list = new Edge*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
		list[i] = nullptr;
	for (long long i = 0, vertex = 0, nextID = 0, value = 0; i < edges; ++i)
	{
		fin >> vertex >> nextID >> value;
		list[vertex] = new Edge(vertex, nextID, value, list[vertex]);
	}
}

GraphList::GraphList(long long vertexCount, long long edgeCount) : vertexes(vertexCount)
{
	list = new Edge*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
		list[i] = nullptr;
	for (long long i = 0, vertex = 0, nextVertex = 0; i < edgeCount; ++i)
	{
		do
		{
			vertex = rand() % vertexes;
			nextVertex = rand() % vertexes;
			if (nextVertex != vertex && !getEdge(vertex, nextVertex))
			{
				list[vertex] = new Edge(vertex, nextVertex, 1 + rand() % 25, list[vertex]);
				break;
			}
		} while (true);
	}
}

void GraphList::set(long long vertexCount, long long edgeCount)
{
	for (long long i = 0; i < vertexes; ++i)
	{
		Edge * temp = list[i], *next = nullptr;
		while (temp)
		{
			next = temp->getNext();
			delete temp;
			temp = next;
		}
	}
	delete[] list;
	vertexes = vertexCount;
	list = new Edge*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
		list[i] = nullptr;
	for (long long i = 0, vertex = 0, nextVertex = 0; i < edgeCount; ++i)
	{
		do
		{
			vertex = rand() % vertexes;
			nextVertex = rand() % vertexes;
			if (nextVertex != vertex && !getEdge(vertex, nextVertex))
			{
				list[vertex] = new Edge(vertex, nextVertex, 1 + rand() % 25, list[vertex]);
				break;
			}
		} while (true);
	}
}

void GraphList::show() const
{
	Edge * temp = nullptr;
	for (long long i = 0; i < vertexes; ++i)
	{
		std::cout << i;
		if (list[i])
		{
			std::cout << ": -$" << list[i]->getValue() << "->" << list[i]->getNextVertex();
			temp = list[i]->getNext();
			while (temp)
			{
				std::cout << ", -$" << temp->getValue() << "->" << temp->getNextVertex();
				temp = temp->getNext();
			}
			std::cout << std::endl;
		}
		else
			std::cout << ".\n";
	}
}

MinimalSpanTree * GraphList::minimalTreePrim() const
{
	bool * visited = new bool[vertexes];
	MinimalSpanTree graph(vertexes);
	for (long long i = 0; i < vertexes; ++i)
	{
		visited[i] = false;
		for (Edge * temp = list[i]; temp; temp = temp->getNext())
			graph.addEdge(*temp);
	}
	visited[0] = true;
	Edge edge;
	Queue list(edges);
	MinimalSpanTree * tree = new MinimalSpanTree(vertexes);
	bool skip = false;
	for (long long i = 1, vertex = 0; i < vertexes; ++i)
	{
		for (Node * temp = graph.getNeighbours(vertex); temp; temp = temp->getNext())
			if (!visited[temp->getNextVertex()])
			{
				edge.setStartVertex(vertex);
				edge.setNextVertex(temp->getNextVertex(), nullptr);
				edge.setValue(temp->getWeight());
				list.push(edge);
			}
		do
		{
			if (!list.size())
			{
				skip = true;
				break;
			}
			edge = list.front();
			list.pop();
		} while (visited[edge.getNextVertex()]);
		if (skip)
		{
			skip = false;
			continue;
		}
		tree->addEdge(edge);
		vertex = edge.getNextVertex();
		visited[vertex] = true;
	}
	delete[] visited;
	return tree;
}

MinimalSpanTree * GraphList::minimalTreeKruskal() const
{
	DisjunctiveSet set(vertexes);
	Queue tempList(edges);
	for (long long i = 0; i < vertexes; ++i)
	{
		set.makeSet(i);
		for (Edge * temp = list[i]; temp; temp = temp->getNext())
			tempList.push(*temp);
	}
	Edge temp;
	MinimalSpanTree * tree = new MinimalSpanTree(vertexes);
	bool skip = false;
	for (long long i = 1; i < vertexes; ++i)
	{
		do
		{
			if (!tempList.size())
			{
				skip = true;
				break;
			}
			temp = tempList.front();
			tempList.pop();
		} while (set.findSet(temp.getStartVertex()) == set.findSet(temp.getNextVertex()));
		if (skip)
		{
			skip = false;
			continue;
		}
		tree->addEdge(temp);
		set.unionSets(temp);
	}
	return tree;
}

Path * GraphList::findPathDijkstra(long long first, long long last) const
{
	long long * costs = new long long[vertexes], * predecessors = new long long[vertexes];
	bool * visited = new bool[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		costs[i] = MAXLONGLONG;
		predecessors[i] = -1;
		visited[i] = false;
	}
	costs[first] = 0;
	for (long long i = 0, j = 0, leastCostID; i < vertexes; ++i)
	{
		for (j = 0; visited[j]; ++j);
		for (leastCostID = j++; j < vertexes; ++j)
			if (!visited[j] && costs[j] < costs[leastCostID]) 
				leastCostID = j;
		visited[leastCostID] = true;
		for (Edge * temp = list[leastCostID]; temp; temp = temp->getNext())
			if (!visited[temp->getNextVertex()] && costs[temp->getNextVertex()] > costs[leastCostID] + temp->getValue())
			{
				costs[temp->getNextVertex()] = costs[leastCostID] + temp->getValue();
				predecessors[temp->getNextVertex()] = leastCostID;
			}
	}
	Path * output = new Path;
	output->tab = (long long*)calloc(vertexes, sizeof(long long));
	for (long long j = last; j > -1; j = predecessors[j])
		output->tab[output->count++] = j;
	output->tab = (long long*)realloc(output->tab, output->count * sizeof(long long));
	output->value = costs[last];
	delete[] costs;
	delete[] predecessors;
	delete[] visited;
	return output;
}

Path * GraphList::findPathFordBellman(long long first, long long last) const
{
	long long * costs = new long long[vertexes], * predecessors = new long long[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		costs[i] = MAXLONGLONG;
		predecessors[i] = -1;
	}
	costs[first] = 0;
	for (long long i = 1; i < vertexes; ++i)
	{
		for (long long j = 0; j < vertexes; ++j)
			for (Edge * temp = list[j]; temp; temp = temp->getNext())
				if (costs[j] != MAXLONGLONG && costs[temp->getNextVertex()] > costs[j] + temp->getValue())
				{
					costs[temp->getNextVertex()] = costs[j] + temp->getValue();
					predecessors[temp->getNextVertex()] = j;
				}
	}
	Path * output = new Path;
	output->tab = (long long*)calloc(vertexes, sizeof(long long));
	for (long long j = last; j > -1; j = predecessors[j])
		output->tab[output->count++] = j;
	output->tab = (long long*)realloc(output->tab, output->count * sizeof(long long));
	output->value = costs[last];
	delete[] costs;
	delete[] predecessors;
	return output;
}

FlowList * GraphList::maximalFlowBFS(long long source, long long target) const
{
	bool escape = false;
	Edge ** list = new Edge*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		list[i] = nullptr;
		for (Edge * temp = this->list[i]; temp; temp = temp->getNext())
			list[i] = new Edge(i, temp->getNextVertex(), temp->getValue(), list[i]);
	}
	for (long long i = 0; i < vertexes; ++i)
	{
		for (Edge * x = list[i]; x; x = x->getNext())
		{
			escape = false;
			for (Edge * temp = list[x->getNextVertex()]; temp; temp = temp->getNext())
				if (temp->getNextVertex() == i)
				{
					escape = true; 
					break;
				}
			if (escape) 
				continue;
			list[x->getNextVertex()] = new Edge(x->getNextVertex(), i, 0, list[x->getNextVertex()]);
		}
	}
	escape = false;
	bool connection = true;
	long long maxFlow = 0, bandwidth = 0, j = 0;
	long long * predecessors = new long long[vertexes], * residualBandwidth = new long long[vertexes];
	List queue;
	while (true)
	{
		for (long long i = 0; i < vertexes; ++i)
			predecessors[i] = -1;
		residualBandwidth[source] = MAXLONGLONG;
		while (!queue.empty())
			queue.pop();
		queue.pushBack(source);
		while (!queue.empty())
		{
			escape = false;
			j = queue.front();
			queue.pop();
			for (Edge * x = list[j]; x; x = x->getNext())
			{
				bandwidth = x->getValue() - x->getFlow();
				if (bandwidth && predecessors[x->getNextVertex()] == -1)
				{
					predecessors[x->getNextVertex()] = j;
					residualBandwidth[x->getNextVertex()] = bandwidth < residualBandwidth[j] ? bandwidth : residualBandwidth[j];
					if (x->getNextVertex() == target)
					{
						escape = true;
						break;
					}
					else 
						queue.pushBack(x->getNextVertex());
				}
			}
			if (escape) 
				break;
		}
		maxFlow += residualBandwidth[target];
		for (long long i = target; i != source; i = j)
		{
			j = predecessors[i];
			if (j == -1)
			{
				connection = false;
				break;
			}
			for (Edge * z = list[j]; z; z = z->getNext())
				if (z->getNextVertex() == i)
				{
					z->setFlow(z->getFlow() + residualBandwidth[target]);
					break;
				}
			for (Edge * z = list[i]; z; z = z->getNext())
				if (z->getNextVertex() == j)
				{
					z->setFlow(z->getFlow() - residualBandwidth[target]);
					break;
				}
		}
		if (!escape)
			break;
		if (connection != escape)
			connection = escape;
		else
			break;
	}
	delete[] predecessors;
	delete[] residualBandwidth;
	FlowList * output = new FlowList;
	output->list = list;
	output->connection = connection;
	output->size = vertexes;
	output->value = maxFlow;
	return output;
}

FlowList * GraphList::maximalFlowDFS(long long source, long long target) const
{
	bool escape = false;
	Edge ** list = new Edge*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		list[i] = nullptr;
		for (Edge * temp = this->list[i]; temp; temp = temp->getNext())
			list[i] = new Edge(i, temp->getNextVertex(), temp->getValue(), list[i]);
	}
	for (long long i = 0; i < vertexes; ++i)
	{
		for (Edge * x = list[i]; x; x = x->getNext())
		{
			escape = false;
			for (Edge * temp = list[x->getNextVertex()]; temp; temp = temp->getNext())
				if (temp->getNextVertex() == i)
				{
					escape = true;
					break;
				}
			if (escape)
				continue;
			list[x->getNextVertex()] = new Edge(x->getNextVertex(), i, 0, list[x->getNextVertex()]);
		}
	}
	escape = false;
	bool connection = true;
	long long maxFlow = 0, bandwidth = 0, j = 0;
	long long * predecessors = new long long[vertexes], *residualBandwidth = new long long[vertexes];
	List stack;
	while (true)
	{
		for (long long i = 0; i < vertexes; ++i)
			predecessors[i] = -1;
		residualBandwidth[source] = MAXLONGLONG;
		while (!stack.empty())
			stack.pop();
		stack.pushFront(source);
		while (!stack.empty())
		{
			escape = false;
			j = stack.front();
			stack.pop();
			for (Edge * x = list[j]; x; x = x->getNext())
			{
				bandwidth = x->getValue() - x->getFlow();
				if (bandwidth && predecessors[x->getNextVertex()] == -1)
				{
					predecessors[x->getNextVertex()] = j;
					residualBandwidth[x->getNextVertex()] = bandwidth < residualBandwidth[j] ? bandwidth : residualBandwidth[j];
					if (x->getNextVertex() == target)
					{
						escape = true;
						break;
					}
					else
						stack.pushFront(x->getNextVertex());
				}
			}
			if (escape)
				break;
		}
		maxFlow += residualBandwidth[target];
		for (long long i = target; i != source; i = j)
		{
			j = predecessors[i];
			if (j == -1)
			{
				connection = false;
				break;
			}
			for (Edge * z = list[j]; z; z = z->getNext())
				if (z->getNextVertex() == i)
				{
					z->setFlow(z->getFlow() + residualBandwidth[target]);
					break;
				}
			for (Edge * z = list[i]; z; z = z->getNext())
				if (z->getNextVertex() == j)
				{
					z->setFlow(z->getFlow() - residualBandwidth[target]);
					break;
				}
		}
		if (!escape)
			break;
		if (connection != escape)
			connection = escape;
		else
			break;
	}
	delete[] predecessors;
	delete[] residualBandwidth;
	FlowList * output = new FlowList;
	output->list = list;
	output->connection = connection;
	output->size = vertexes;
	output->value = maxFlow;
	return output;
}

GraphList::~GraphList()
{
	if (list)
	{
		Edge * temp = nullptr, * next = nullptr;
		for (long long i = 0; i < vertexes; ++i)
		{
			temp = list[i];
			while (temp)
			{
				next = temp->getNext();
				delete temp;
				temp = next;
			}
		}
		delete[] list;
	}
}