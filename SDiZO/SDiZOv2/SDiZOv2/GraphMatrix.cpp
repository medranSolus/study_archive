#include "pch.h"
#include "GraphMatrix.h"
#include "DisjunctiveSet.h"
#include "Queue.h"
#include "List.h"
#include <climits>
#include <iostream>

long long GraphMatrix::getEdge(long long vertex, Connection type, long long startEdge) const
{
	for (long long i = startEdge + 1; i < edges; ++i)
		if (matrix[vertex][i] == type)
			return i;
	return edges;
}

long long GraphMatrix::getVertex(long long edge, Connection type) const
{
	for (long long i = 0; i < vertexes; ++i)
		if (matrix[i][edge] == type)
			return i;
	return vertexes;
}

GraphMatrix::GraphMatrix(std::ifstream & fin)
{
	fin >> edges >> vertexes;
	values = new long long[edges];
	matrix = new Connection*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		matrix[i] = new Connection[edges];
		for (long long j = 0; j < edges; ++j)
			matrix[i][j] = Connection::None;
	}
	for (long long i = 0, temp = 0; i < edges; ++i)
	{
		fin >> temp;
		matrix[temp][i] = Connection::First;
		fin >> temp;
		matrix[temp][i] = Connection::Last;
		fin >> temp;
		values[i] = temp;
	}
}

GraphMatrix::GraphMatrix(long long vertexCount, long long edgeCount) : vertexes(vertexCount), edges(edgeCount)
{
	values = new long long[edges];
	matrix = new Connection*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		matrix[i] = new Connection[edges];
		for (long long j = 0; j < edges; ++j)
			matrix[i][j] = Connection::None;
	}
	long long first = 0, last = 0;
	bool taken = false;
	for (long long i = 0; i < edges; ++i)
	{
		do
		{
			taken = false;
			first = rand() % vertexes;
			last = rand() % vertexes;
			if (first == last)
			{
				taken = true;
				continue;
			}
			for (long long j = 0; j < edges; ++j)
				if (matrix[first][j] == Connection::First && matrix[last][j] == Connection::Last)
				{
					taken = true;
					break;
				}
		} while (taken);
		matrix[first][i] = Connection::First;
		matrix[last][i] = Connection::Last;
		values[i] = 1 + rand() % 25;
	}
}

void GraphMatrix::set(long long vertexCount, long long edgeCount)
{
	for (long long i = 0; i < vertexes; ++i)
		delete[] matrix[i];
	delete[] matrix;
	delete[] values;
	vertexes = vertexCount;
	edges = edgeCount;
	values = new long long[edges];
	matrix = new Connection*[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		matrix[i] = new Connection[edges];
		for (long long j = 0; j < edges; ++j)
			matrix[i][j] = Connection::None;
	}
	long long first = 0, last = 0;
	bool taken = false;
	for (long long i = 0; i < edges; ++i)
	{
		do
		{
			taken = false;
			first = rand() % vertexes;
			last = rand() % vertexes;
			if (first == last)
			{
				taken = true;
				continue;
			}
			for (long long j = 0; j < edges; ++j)
				if (matrix[first][j] == Connection::First && matrix[last][j] == Connection::Last)
				{
					taken = true;
					break;
				}
		} while (taken);
		matrix[first][i] = Connection::First;
		matrix[last][i] = Connection::Last;
		values[i] = 1 + rand() % 25;
	}
}

void GraphMatrix::show() const
{
	std::cout << "$\t| ";
	for (long long i = 0; i < edges; ++i)
		std::cout << values[i] << "\t| ";
	std::cout << std::endl;
	for (long long i = 0; i < vertexes; ++i)
	{
		std::cout << i << "\t| ";
		for (long long j = 0; j < edges; ++j)
			std::cout << (matrix[i][j] == Connection::None ? 0 : (matrix[i][j] == Connection::First ? -1 : 1)) << "\t| ";
		std::cout << std::endl;
	}
}

void GraphMatrix::save(std::ofstream & fout) const
{
	fout << edges << ' ' << vertexes;
	long long first = 0, last = 0;
	bool found = false;
	for (long long i = 0; i < edges; ++i)
	{
		for (long long j = 0; j < vertexes; ++j)
		{
			if (matrix[j][i] == Connection::First)
			{
				first = j;
				if (found)
					break;
				else
					found = true;
			}
			else if (matrix[j][i] == Connection::Last)
			{
				last = j;
				if (found)
					break;
				else
					found = true;
			}
		}
		found = false;
		fout << std::endl << first << ' ' << last << ' ' << values[i];
	}
}

MinimalSpanTree * GraphMatrix::minimalTreePrim() const
{
	bool * visited = new bool[vertexes];
	MinimalSpanTree graph(vertexes);
	for (long long i = 0; i < vertexes; ++i)
	{
		visited[i] = false;
		for (long long edge = getEdge(i, Connection::First), nextVertex; edge < edges; edge = getEdge(i, Connection::First, edge))
			graph.addEdge(Edge(i, getVertex(edge, Connection::Last), values[edge]));
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

MinimalSpanTree * GraphMatrix::minimalTreeKruskal() const
{
	DisjunctiveSet set(vertexes);
	Queue list(edges);
	for (long long i = 0; i < vertexes; ++i)
	{
		set.makeSet(i);
		for (long long edge = getEdge(i, Connection::First), nextVertex; edge < edges; edge = getEdge(i, Connection::First, edge))
			list.push(Edge(i, getVertex(edge, Connection::Last), values[edge]));
	}
	MinimalSpanTree * tree = new MinimalSpanTree(vertexes);
	Edge temp;
	bool skip = false;
	for (long long i = 1; i < vertexes; ++i)
	{
		do
		{
			if (!list.size())
			{
				skip = true;
				break;
			}
			temp = list.front();
			list.pop();
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

Path * GraphMatrix::findPathDijkstra(long long first, long long last) const
{
	long long * costs = new long long[vertexes], * predecessors = new long long[vertexes];
	bool * visited = new bool[vertexes];
	for(long long i = 0; i < vertexes; ++i)
	{
		costs[i] = MAXLONGLONG;
		predecessors[i] = -1;
		visited[i] = false;
	}
	costs[first] = 0;
	for(long long i = 0, j = 0, leastCostID; i < vertexes; ++i)
	{
		for(j = 0; visited[j]; ++j);
		for(leastCostID = j++; j < vertexes; ++j)
			if(!visited[j] && costs[j] < costs[leastCostID]) 
				leastCostID = j;
		visited[leastCostID] = true;
		for (long long edge = getEdge(leastCostID, Connection::First), nextVertex; edge < edges; edge = getEdge(leastCostID, Connection::First, edge))
		{
			nextVertex = getVertex(edge, Connection::Last);
			if (!visited[nextVertex] && costs[nextVertex] > costs[leastCostID] + values[edge])
			{
				costs[nextVertex] = costs[leastCostID] + values[edge];
				predecessors[nextVertex] = leastCostID;
			}
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

Path * GraphMatrix::findPathFordBellman(long long first, long long last) const
{
	long long * costs = new long long[vertexes], *predecessors = new long long[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		costs[i] = MAXLONGLONG;
		predecessors[i] = -1;
	}
	costs[first] = 0;
	for (long long i = 1; i < vertexes; ++i)
	{
		for (long long j = 0; j < vertexes; ++j)
			for (long long edge = getEdge(j, Connection::First), nextVertex; edge < edges; edge = getEdge(j, Connection::First, edge))
			{
				nextVertex = getVertex(edge, Connection::Last);
				if (costs[j] != MAXLONGLONG && costs[nextVertex] > costs[j] + values[edge])
				{
					costs[nextVertex] = costs[j] + values[edge];
					predecessors[nextVertex] = j;
				}
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

FlowMatrixes * GraphMatrix::maximalFlowBFS(long long source, long long target) const
{
	long long ** bandwidthMatrix = new long long*[vertexes], ** flowNettoMatrix = new long long*[vertexes];
	long long * predecessors = new long long[vertexes], *residualBandwidth = new long long[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		bandwidthMatrix[i] = new long long[vertexes];
		flowNettoMatrix[i] = new long long[vertexes];
		for (long long j = 0; j < vertexes; ++j)
			bandwidthMatrix[i][j] = flowNettoMatrix[i][j] = 0;
	}
	for (long long i = 0; i < vertexes; ++i)
		for (long long edge = getEdge(i, Connection::First), nextVertex; edge < edges; edge = getEdge(i, Connection::First, edge))
			bandwidthMatrix[i][getVertex(edge, Connection::Last)] = values[edge];
	List queue;
	long long maxFlow = 0, bandwidth = 0, x = 0, i = 0;
	bool escape = false, connection = true;
	while (!escape)
	{
		for (long long i = 0; i < vertexes; ++i)
			predecessors[i] = -1;
		predecessors[source] = -2;
		residualBandwidth[source] = MAXLONGLONG;
		while (!queue.empty())
			queue.pop();
		queue.pushBack(source);
		escape = false;
		while (!queue.empty())
		{
			x = queue.front();
			queue.pop();
			for (long long j = 0; j < vertexes; ++j)
			{
				bandwidth = bandwidthMatrix[x][j] - flowNettoMatrix[x][j];
				if (bandwidth && predecessors[j] == -1)
				{
					predecessors[j] = x;
					residualBandwidth[j] = residualBandwidth[x] > bandwidth ? bandwidth : residualBandwidth[x];
					if (j == target)
					{
						maxFlow += residualBandwidth[target];
						i = j;
						while (i != source)
						{
							x = predecessors[i];
							flowNettoMatrix[x][i] += residualBandwidth[target];
							flowNettoMatrix[i][x] -= residualBandwidth[target];
							i = x;
						}
						escape = true;
						break;
					}
					queue.pushBack(j);
				}
			}
			if (escape)
				break;
		}
		if (connection != escape)
			connection = escape;
		else
			break;
	}
	delete[] predecessors;
	delete[] residualBandwidth;
	FlowMatrixes * output = new FlowMatrixes();
	output->bandwidthMatrix = bandwidthMatrix;
	output->flowNettoMatrix = flowNettoMatrix;
	output->connection = connection;
	output->size = vertexes;
	output->value = maxFlow;
	return output;
}

FlowMatrixes * GraphMatrix::maximalFlowDFS(long long source, long long target) const
{
	long long ** bandwidthMatrix = new long long*[vertexes], ** flowNettoMatrix = new long long*[vertexes];
	long long * predecessors = new long long[vertexes], *residualBandwidth = new long long[vertexes];
	for (long long i = 0; i < vertexes; ++i)
	{
		bandwidthMatrix[i] = new long long[vertexes];
		flowNettoMatrix[i] = new long long[vertexes];
		for (long long j = 0; j < vertexes; ++j)
			bandwidthMatrix[i][j] = flowNettoMatrix[i][j] = 0;
	}
	for (long long i = 0; i < vertexes; ++i)
		for (long long edge = getEdge(i, Connection::First), nextVertex; edge < edges; edge = getEdge(i, Connection::First, edge))
			bandwidthMatrix[i][getVertex(edge, Connection::Last)] = values[edge];
	List stack;
	long long maxFlow = 0, bandwidth = 0, x = 0, i = 0;
	bool escape = false, connection = true;
	while (!escape)
	{
		for (long long i = 0; i < vertexes; ++i)
			predecessors[i] = -1;
		predecessors[source] = -2;
		residualBandwidth[source] = MAXLONGLONG;
		while (!stack.empty())
			stack.pop();
		stack.pushFront(source);
		escape = false;
		while (!stack.empty())
		{
			x = stack.front();
			stack.pop();
			for (long long j = 0; j < vertexes; ++j)
			{
				bandwidth = bandwidthMatrix[x][j] - flowNettoMatrix[x][j];
				if (bandwidth && predecessors[j] == -1)
				{
					predecessors[j] = x;
					residualBandwidth[j] = residualBandwidth[x] > bandwidth ? bandwidth : residualBandwidth[x];
					if (j == target)
					{
						maxFlow += residualBandwidth[target];
						i = j;
						while (i != source)
						{
							x = predecessors[i];
							flowNettoMatrix[x][i] += residualBandwidth[target];
							flowNettoMatrix[i][x] -= residualBandwidth[target];
							i = x;
						}
						escape = true;
						break;
					}
					stack.pushFront(j);
				}
			}
			if (escape)
				break;
		}
		if (connection != escape)
			connection = escape;
		else
			break;
	}
	delete[] predecessors;
	delete[] residualBandwidth;
	FlowMatrixes * output = new FlowMatrixes();
	output->bandwidthMatrix = bandwidthMatrix;
	output->flowNettoMatrix = flowNettoMatrix;
	output->connection = connection;
	output->size = vertexes;
	output->value = maxFlow;
	return output;
}

GraphMatrix::~GraphMatrix()
{
	if (matrix)
	{
		for (long long i = 0; i < vertexes; ++i)
			if (matrix[i])
				delete[] matrix[i];
		delete[] matrix;
	}
	if (values)
		delete[] values;
}