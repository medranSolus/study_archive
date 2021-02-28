#include "pch.h"
#include <climits>
#include <string>
#include <fstream>
#include "TreeAVL.h"
namespace DataTypes
{
	std::string TreeAVL::frameRight, TreeAVL::frameLeft, TreeAVL::frameParent;

	void TreeAVL::purge(Node * node)
	{
		if (node != nullptr)
		{
			purge(node->getRightChild());
			purge(node->getLeftChild());
			delete node;
		}
	}
	
	void TreeAVL::rotateRR(Node * node)
	{
		Node * temp = node->getRightChild(), * parent = node->getParent();
		node->setRightChild(temp->getLeftChild());
		if (node->getRightChild() != nullptr)
			node->getRightChild()->setParent(node);
		temp->setLeftChild(node);
		temp->setParent(parent);
		node->setParent(temp);
		if (parent != nullptr)
		{
			if (parent->getLeftChild() == node)
				parent->setLeftChild(temp);
			else
				parent->setRightChild(temp);
		}
		else
			root = temp;
		if (temp->getBalanceFactor() == -1)
		{
			node->setBalance(0);
			temp->setBalance(0);
		}
		else
		{
			node->setBalance(-1);
			temp->setBalance(1);
		}
	}

	void TreeAVL::rotateLL(Node * node)
	{
		Node * temp = node->getLeftChild(), *parent = node->getParent();
		node->setLeftChild(temp->getRightChild());
		if (node->getLeftChild() != nullptr)
			node->getLeftChild()->setParent(node);
		temp->setRightChild(node);
		temp->setParent(parent);
		node->setParent(temp);
		if (parent != nullptr)
		{
			if (parent->getLeftChild() == node)
				parent->setLeftChild(temp);
			else
				parent->setRightChild(temp);
		}
		else
			root = temp;
		if (temp->getBalanceFactor() == 1)
		{
			node->setBalance(0);
			temp->setBalance(0);
		}
		else
		{
			node->setBalance(1);
			temp->setBalance(-1);
		}
	}

	void TreeAVL::rotateRL(Node * node)
	{
		Node * temp1 = node->getRightChild(), * temp2 = temp1->getLeftChild(), * parent = node->getParent();
		temp1->setLeftChild(temp2->getRightChild());
		if (temp1->getLeftChild() != nullptr)
			temp1->getLeftChild()->setParent(temp1);
		node->setRightChild(temp2->getLeftChild());
		if (node->getRightChild() != nullptr)
			node->getRightChild()->setParent(node);
		temp2->setLeftChild(node);
		temp2->setRightChild(temp1);
		temp1->setParent(temp2);
		node->setParent(temp2);
		temp2->setParent(parent);
		if (parent != nullptr)
		{
			if (parent->getLeftChild() == node)
				parent->setLeftChild(temp2);
			else
				parent->setRightChild(temp2);
		}
		else 
			root = temp2;

		if (temp2->getBalanceFactor() == -1)
			node->setBalance(1);
		else 
			node->setBalance(0);
		if (temp2->getBalanceFactor() == 1) 
			temp1->setBalance(-1);
		else 
			temp1->setBalance(0);
		temp2->setBalance(0);
	}

	void TreeAVL::rotateLR(Node * node)
	{
		Node * temp1 = node->getLeftChild(), * temp2 = temp1->getRightChild(), *parent = node->getParent();
		temp1->setRightChild(temp2->getLeftChild());
		if (temp1->getRightChild() != nullptr)
			temp1->getRightChild()->setParent(temp1);
		node->setLeftChild(temp2->getRightChild());
		if (node->getLeftChild() != nullptr) 
			node->getLeftChild()->setParent(node);
		temp2->setRightChild(node);
		temp2->setLeftChild(temp1);
		temp1->setParent(temp2);
		node->setParent(temp2);
		temp2->setParent(parent);
		if (parent != nullptr)
		{
			if (parent->getLeftChild() == node) 
				parent->setLeftChild(temp2); 
			else 
				parent->setRightChild(temp2);
		}
		else 
			root = temp2;
		if (temp2->getBalanceFactor() == 1) 
			node->setBalance(-1);
		else 
			node->setBalance(0);
		if (temp2->getBalanceFactor() == -1) 
			temp1->setBalance(1);
		else 
			temp1->setBalance(0);
		temp2->setBalance(0);
	}

	void TreeAVL::printTree(std::string frameTrace, std::string toDisplay, Node * node) const
	{
		std::string displayFrame;
		frameRight = frameLeft = frameParent = "  ";
		frameRight[0] = 218;
		frameRight[1] = 196;
		frameLeft[0] = 192;
		frameLeft[1] = 196;
		frameParent[0] = 179;
		if (node != nullptr)
		{
			displayFrame = frameTrace;
			if (toDisplay == frameRight)
				displayFrame[displayFrame.length() - 2] = ' ';
			printTree(displayFrame + frameParent, frameRight, node->getRightChild());
			displayFrame = displayFrame.substr(0, frameTrace.length() - 2);
			std::cout << displayFrame << toDisplay;
			std::cout << node->getKey() << '(' << node->getBalanceFactor() << ')' << std::endl;
			displayFrame = frameTrace;
			if (toDisplay == frameLeft)
				displayFrame[displayFrame.length() - 2] = ' ';
			printTree(displayFrame + frameParent, frameLeft, node->getLeftChild());
		}
	}

	bool TreeAVL::containsKey(Node * node, long key) const
	{
		while (node != nullptr)
		{
			if (node->getKey() == key)
				return true;
			node->getKey() < key ? node = node->getRightChild() : node = node->getLeftChild();
		}
		return false;
	}

	TreeAVL::Node * TreeAVL::searchKey(Node * node, long key)
	{
		while (node != nullptr)
		{
			if (node->getKey() == key)
				return node;
			node->getKey() < key ? node = node->getRightChild() : node = node->getLeftChild();
		}
		return nullptr;
	}

	TreeAVL::Node * TreeAVL::findPredecessor(Node * node)
	{
		if (node != nullptr)
		{
			if (node->getLeftChild() != nullptr)
			{
				node = node->getLeftChild();
				while (node->getRightChild() != nullptr)
					node = node->getLeftChild();
			}
			else
			{
				Node * temp = nullptr;
				do
				{
					temp = node;
					node = node->getParent();
				} while (node && node->getLeftChild() != temp);
			}
		}
		return node;
	}

	TreeAVL::Node * TreeAVL::erase(Node * node)
	{
		if (node == nullptr)
			return nullptr;
		Node * parent = nullptr;
		bool nest;
		if (node->getLeftChild() != nullptr && node->getRightChild() != nullptr)
		{
			parent = erase(findPredecessor(node));
			nest = false;
		}
		else
		{
			if (node->getLeftChild())
			{
				parent = node->getLeftChild(); 
				node->setLeftChild(nullptr);
			}
			else
			{
				parent = node->getRightChild(); 
				node->setRightChild(nullptr);
			}
			node->setBalance(0);
			nest = true;
		}
		if (parent != nullptr)
		{
			parent->setParent(node->getParent());
			parent->setLeftChild(node->getLeftChild());  
			if (parent->getLeftChild() != nullptr) 
				parent->getLeftChild()->setParent(parent);
			parent->setRightChild(node->getRightChild()); 
			if (parent->getRightChild() != nullptr)  
				parent->getRightChild()->setParent(parent);
			parent->setBalance(node->getBalanceFactor());
		}
		if (node->getParent())
		{
			if (node->getParent()->getLeftChild() == node) 
				node->getParent()->setLeftChild(parent); 
			else 
				node->getParent()->setRightChild(parent);
		}
		else 
			root = parent;
		if (nest)
		{
			Node * swap = parent;
			parent = node->getParent();
			while (parent != nullptr)
			{
				if (!parent->getBalanceFactor())
				{
					if (parent->getLeftChild() == swap)  
						parent->setBalance(-1); 
					else 
						parent->setBalance(1);
					break;
				}
				else
				{
					if ((parent->getBalanceFactor() == 1 && parent->getLeftChild() == swap) || (parent->getBalanceFactor() == -1 && parent->getRightChild() == swap))
					{
						parent->setBalance(0);
						swap = parent; 
						parent = parent->getParent();
					}
					else
					{
						Node * temp = nullptr;
						if (parent->getLeftChild() == swap)  
							temp = parent->getRightChild();
						else 
							temp = parent->getLeftChild();
						if (!temp->getBalanceFactor())
						{
							if (parent->getBalanceFactor() == 1) 
								rotateLL(parent); 
							else 
								rotateRR(parent);
							break;
						}
						else if (parent->getBalanceFactor() == temp->getBalanceFactor())
						{
							if (parent->getBalanceFactor() == 1) 
								rotateLL(parent); 
							else 
								rotateRR(parent);
							swap = temp;
							parent = temp->getParent();
						}
						else
						{
							if (parent->getBalanceFactor() == 1) 
								rotateLR(parent); 
							else 
								rotateRL(parent);
							swap = parent->getParent(); 
							parent = swap->getParent();
						}
					}
				}
			}
		}
		return node;
	}

	TreeAVL::TreeAVL(const std::string & fileName)
	{
		std::ifstream fin(fileName);
		if (fin.good())
		{
			long count = 0;
			fin >> count;
			if (count)
			{
				for (long i = 0, data = 0; i < count; ++i)
				{
					fin >> data;
					add(data);
				}
			}
		}
		fin.close();
	}

	void TreeAVL::add(long key)
	{
		if (containsKey(root, key))
			return;
		Node * node = new Node(key, 0, nullptr, nullptr, nullptr), * parent = root;
		/*
		for (long i = 0; i < LONG_MAX; ++i)
			if (searchKey(root, i) == nullptr)
			{
				node = new Node(i, value, 0, nullptr, nullptr, nullptr);
				break;
			}*/
		if (parent == nullptr)
			root = node;
		else
		{
			for(;;)
			{
				if (node->getKey() < parent->getKey())
				{
					if (parent->getLeftChild() == nullptr)
					{
						parent->setLeftChild(node);
						break;
					}
					parent = parent->getLeftChild();
				}
				else
				{
					if (parent->getRightChild() == nullptr)
					{
						parent->setRightChild(node);
						break;
					}
					parent = parent->getRightChild();
				}
			}
			node->setParent(parent);
			if (parent->getBalanceFactor()) 
				parent->setBalance(0);
			else
			{
				if (parent->getLeftChild() == node)
					parent->setBalance(1);
				else
					parent->setBalance(-1);
				Node * temp = parent->getParent();
				bool balance = true;
				while (temp != nullptr)
				{
					if (temp->getBalanceFactor())
					{
						balance = false;
						break;
					};
					if (temp->getLeftChild() == parent) 
						temp->setBalance(1);
					else             
						temp->setBalance(-1);
					parent = temp;
					temp = temp->getParent();
				}
				if (!balance)
				{
					if (temp->getBalanceFactor() == 1)
					{
						if (temp->getRightChild() == parent) 
							temp->setBalance(0);
						else if (parent->getBalanceFactor() == -1) 
							rotateLR(temp);
						else                 
							rotateLL(temp);
					}
					else
					{
						if (temp->getLeftChild() == parent) 
							temp->setBalance(0);
						else if (parent->getBalanceFactor() == 1) 
							rotateRL(temp);
						else
							rotateRR(temp);
					}
				}
			}
		}
	}

	void TreeAVL::erase(long key)
	{
		Node * toRemove = erase(searchKey(root, key));
		if (toRemove != nullptr)
			delete toRemove;
	}

	void TreeAVL::show() const
	{
		if (root != nullptr)
			printTree("", "", root);
		else
			std::cout << "Drzewo AVL puste.\n";
	}

	void TreeAVL::clear()
	{
		if (root != nullptr)
			purge(root);
		root = nullptr;
	}

	bool TreeAVL::contains(long key) const
	{
		if (root != nullptr)
			return containsKey(root, key);
		return false;
	}
	
	TreeAVL::~TreeAVL()
	{
		if (root != nullptr)
			delete root;
	}
}