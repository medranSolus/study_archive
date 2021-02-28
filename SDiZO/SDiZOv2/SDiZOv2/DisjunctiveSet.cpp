#include "pch.h"
#include "DisjunctiveSet.h"

DisjunctiveSet::Node::Node(long long upNode, long long nodeRank) : up(upNode), rank(nodeRank) {}

long long DisjunctiveSet::findSet(long long vertex)
{
	if (set[vertex].getUpNode() != vertex)
		set[vertex].setUpNode(findSet(set[vertex].getUpNode()));
	return set[vertex].getUpNode();
}

void DisjunctiveSet::makeSet(long long vertex)
{
	set[vertex].setUpNode(vertex);
	set[vertex].setRank(0);
}

void DisjunctiveSet::unionSets(Edge edge)
{
	long long first = findSet(edge.getStartVertex()), last = findSet(edge.getNextVertex());
	if (first != last)
	{
		if (set[first].getRank() > set[last].getRank())
			set[last].setUpNode(first);
		else
		{
			set[first].setUpNode(last);
			if (set[first].getRank() == set[last].getRank())
				set[last].incrementRank();
		}
	}
}