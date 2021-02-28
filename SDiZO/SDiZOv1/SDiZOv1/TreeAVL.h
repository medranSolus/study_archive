#pragma once
#include <iostream>
#include "ITree.h"
namespace DataTypes
{
	///<summary>
	///Klasa odpowiedzialna za reprezentacjê drzewa AVL
	///</summary>
	class TreeAVL : public ITree
	{
		class Node
		{
			long key = 0;
			short balance = 0;
			Node * parent = nullptr;
			Node * childLeft = nullptr;
			Node * childRight = nullptr;
		public:
			Node() {}
			Node(long newKey, short balanceFactor, Node * parentNode, Node * leftChild, Node * rightChild) : key(newKey), balance(balanceFactor), parent(parentNode), childLeft(leftChild), childRight(rightChild) {}

			inline long getKey() const { return key; }
			inline short getBalanceFactor() const { return balance; }
			inline Node * getParent() const { return parent; }
			inline Node * getLeftChild() const { return childLeft; }
			inline Node * getRightChild() const { return childRight; }

			inline void setKey(long newKey) { key = newKey; }
			inline void setBalance(short balanceFactor) { balance = balanceFactor; }
			inline void setParent(Node * parentNode) { parent = parentNode; }
			inline void setLeftChild(Node * leftChild) { childLeft = leftChild; }
			inline void setRightChild(Node * rightChild) { childRight = rightChild; }
		};

		static std::string frameRight, frameLeft, frameParent;
		Node * root = nullptr;

		void purge(Node * node);
		void rotateRR(Node * node);
		void rotateLL(Node * node);
		void rotateRL(Node * node);
		void rotateLR(Node * node);
		void printTree(std::string frameTrace, std::string toDisplay, Node * node) const;
		bool containsKey(Node * node, long key) const;
		Node * searchKey(Node * node, long key);
		Node * findPredecessor(Node * node);
		Node * erase(Node * node);
	public:
		TreeAVL() {}
		TreeAVL(const std::string & fileName);

		void add(long key);
		void erase(long key);
		void show() const;
		void clear();
		bool contains(long key) const;

		~TreeAVL();
	};
}