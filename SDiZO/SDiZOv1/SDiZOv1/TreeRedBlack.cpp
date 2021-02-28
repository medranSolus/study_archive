#include "pch.h"
#include <fstream>
#include <climits>
#include <string>
#include "TreeRedBlack.h"
namespace DataTypes
{
	TreeRedBlack::Node::Node(long newKey, Color nodeColor, Node * parentNode, Node * leftChild, Node * rightChild) : key(newKey), color(nodeColor), parent(parentNode), childLeft(leftChild), childRight(rightChild) {}
	
	std::string TreeRedBlack::frameRight, TreeRedBlack::frameLeft, TreeRedBlack::frameParent;
	HANDLE TreeRedBlack::consoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);
	TreeRedBlack::Node TreeRedBlack::sentinel;

	void TreeRedBlack::printTree(std::string frameTrace, std::string toDisplay, Node * node) const
	{
		std::string displayFrame;
		frameRight = frameLeft = frameParent = "  ";
		frameRight[0] = 218;
		frameRight[1] = 196;
		frameLeft[0] = 192;
		frameLeft[1] = 196;
		frameParent[0] = 179;
		if (node != &sentinel)
		{
			displayFrame = frameTrace;
			if (toDisplay == frameRight) 
				displayFrame[displayFrame.length() - 2] = ' ';
			printTree(displayFrame + frameParent, frameRight, node->getRightChild());
			displayFrame = displayFrame.substr(0, frameTrace.length() - 2);
			std::cout << displayFrame << toDisplay;
			if (node->getColor() == Red)
				setDisplayColor(Red);
			else
				setDisplayColor(Black);
			std::cout << node->getKey() << std::endl;
			if (node->getColor() == Red)
				setDisplayColor(Black);
			displayFrame = frameTrace;
			if (toDisplay == frameLeft) 
				displayFrame[displayFrame.length() - 2] = ' ';
			printTree(displayFrame + frameParent, frameLeft, node->getLeftChild());
		}
	}
	
	void TreeRedBlack::purge(Node * node)
	{
		if (node != nullptr && node != &sentinel)
		{
			purge(node->getRightChild());
			purge(node->getLeftChild());
			delete node;
		}
	}

	void TreeRedBlack::rotateLeft(Node * node)
	{
		Node * swapRight = node->getRightChild(), *swapRoot;
		if (swapRight != &sentinel)
		{
			swapRoot = node->getParent();
			node->setRightChild(swapRight->getLeftChild());
			if (node->getRightChild() != &sentinel) 
				node->getRightChild()->setParent(node);
			swapRight->setLeftChild(node);
			swapRight->setParent(swapRoot);
			node->setParent(swapRight);
			if (swapRoot != &sentinel)
			{
				if (swapRoot->getLeftChild() == node)
					swapRoot->setLeftChild(swapRight);
				else
					swapRoot->setRightChild(swapRight);
			}
			else 
				root = swapRight;
		}
	}

	void TreeRedBlack::rotateRight(Node * node)
	{
		Node * swapLeft = node->getLeftChild(), *swapRoot;
		if (swapLeft != &sentinel)
		{
			swapRoot = node->getParent();
			node->setLeftChild(swapLeft->getRightChild());
			if (node->getLeftChild() != &sentinel)
				node->getLeftChild()->setParent(node);
			swapLeft->setRightChild(node);
			swapLeft->setParent(swapRoot);
			node->setParent(swapLeft);
			if (swapRoot != &sentinel)
			{
				if (swapRoot->getLeftChild() == node)
					swapRoot->setLeftChild(swapLeft);
				else
					swapRoot->setRightChild(swapLeft);
			}
			else
				root = swapLeft;
		}
	}

	void TreeRedBlack::erase(Node * node)
	{
		if (node == nullptr || node == &sentinel)
			return;
		Node * toDelete = nullptr, * child = nullptr;
		if (node->getLeftChild() == &sentinel || node->getRightChild() == &sentinel)
			toDelete = node;
		else
			toDelete = getSuccessor(node);
		if (toDelete->getLeftChild() != &sentinel)
			child = toDelete->getLeftChild();
		else
			child = toDelete->getRightChild();
		child->setParent(toDelete->getParent());
		if (toDelete->getParent() == &sentinel)
			root = child;
		else if (toDelete == toDelete->getParent()->getLeftChild())
			toDelete->getParent()->setLeftChild(child);
		else
			toDelete->getParent()->setRightChild(child);
		if (toDelete != node)
			node->setKey(toDelete->getKey());
		if (toDelete->getColor() == Black)
		{
			auto processNode = [&child, this](Node * brother, DataTypes::TreeRedBlack::Node * (DataTypes::TreeRedBlack::Node::*child1)() const, DataTypes::TreeRedBlack::Node * (DataTypes::TreeRedBlack::Node::*child2)() const, void (DataTypes::TreeRedBlack::*rotate1)(Node *), void (DataTypes::TreeRedBlack::*rotate2)(Node *)) -> bool
			{
				if (brother->getColor() == Red)
				{
					brother->setColor(Black);
					child->getParent()->setColor(Red);
					(this->*rotate2)(child->getParent());
					brother = (child->getParent()->*child1)();
				}
				if (brother->getLeftChild()->getColor() == Black && brother->getRightChild()->getColor() == Black)
				{
					brother->setColor(Red);
					child = child->getParent();
					return true;
				}
				if ((brother->*child1)()->getColor() == Black)
				{
					(brother->*child2)()->setColor(Black);
					brother->setColor(Red);
					(this->*rotate1)(brother);
					brother = (child->getParent()->*child1)();
				}
				brother->setColor(child->getParent()->getColor());
				child->getParent()->setColor(Black);
				(brother->*child1)()->setColor(Black);
				(this->*rotate2)(child->getParent());
				return false;
			};
			while (child != root && child->getColor() == Black)
			{
				if (child == child->getParent()->getLeftChild())
				{
					if (processNode(child->getParent()->getRightChild(), &DataTypes::TreeRedBlack::Node::getRightChild, &DataTypes::TreeRedBlack::Node::getLeftChild, &DataTypes::TreeRedBlack::rotateRight, &DataTypes::TreeRedBlack::rotateLeft))
						continue;
					break;
				}
				if (processNode(child->getParent()->getLeftChild(), &DataTypes::TreeRedBlack::Node::getLeftChild, &DataTypes::TreeRedBlack::Node::getRightChild, &DataTypes::TreeRedBlack::rotateLeft, &DataTypes::TreeRedBlack::rotateRight))
					continue;
				break;
			}
		}
		child->setColor(Black);
		delete toDelete;
	}

	TreeRedBlack::Node * TreeRedBlack::minimalNode(Node * rootNode) const
	{
		if (rootNode != &sentinel)
			while (rootNode->getLeftChild() != &sentinel) 
				rootNode = rootNode->getLeftChild();
		return rootNode;
	}

	TreeRedBlack::Node * TreeRedBlack::getSuccessor(Node * rootNode) const
	{
		if (rootNode != &sentinel)
		{
			if (rootNode->getRightChild() != &sentinel)
				return minimalNode(rootNode->getRightChild());
			else
			{
				Node * temp = rootNode->getParent();
				while ((temp != &sentinel) && (rootNode == temp->getRightChild()))
				{
					rootNode = temp;
					temp = temp->getParent();
				}
				return temp;
			}
		}
		return &sentinel;
	}

	TreeRedBlack::Node * TreeRedBlack::getNode(long key)
	{
		if (root != nullptr && root != &sentinel)
			return searchKey(root, key);
		return nullptr;
	}

	TreeRedBlack::Node * TreeRedBlack::searchKey(Node * node, long key)
	{
		while (node != &sentinel && node->getKey() != key)
		{
			if (key < node->getKey())
				node = node->getLeftChild();
			else
				node = node->getRightChild();
		}
		if (node == &sentinel) 
			return nullptr;
		return node;
	}

	bool TreeRedBlack::containsKey(Node * node, long key) const
	{
		while (node != nullptr)
		{
			if (node->getKey() == key)
				return true;
			node->getKey() < key ? node = node->getRightChild() : node = node->getLeftChild();
		}
		return false;
	}

	TreeRedBlack::TreeRedBlack(const std::string & fileName)
	{
		std::ifstream fin(fileName);
		if (fin.good())
		{
			long count = 0;
			fin >> count;
			if (count)
			{
				long key = 0;
				fin >> key;
				root = new Node(key, Black, &sentinel, &sentinel, &sentinel);
				for (long i = 1; i < count; ++i)
				{
					fin >> key;
					add(key);
				}
			}
		}
		fin.close();
	}

	bool TreeRedBlack::contains(long key) const
	{
		if (root != nullptr && root != &sentinel)
			return containsKey(root, key);
		return false;
	}

	void TreeRedBlack::show() const
	{
		if (root != &sentinel && root != nullptr)
			printTree("", "", root);
		else
			std::cout << "Drzewo czerwono-czarne puste.\n";
	}

	void TreeRedBlack::clear()
	{
		if (root != nullptr && root != &sentinel)
		{
			purge(root);
			root = &sentinel;
		}
	}

	void TreeRedBlack::add(long key)
	{
		Node * node = new Node(key, Red, root, &sentinel, &sentinel);
		/*
		for (long i = 0; i < LONG_MAX; ++i)
			if (searchKey(root, i) == nullptr)
			{
				node = new Node(i, value, Red, root, &sentinel, &sentinel);
				break;
			}
		*/
		if (node->getParent() == &sentinel)
		{
			root = node;
			root->setColor(Black);
			return;
		}
		else
		{
			for (;;)
			{
				if (node->getKey() < node->getParent()->getKey())
				{
					if (node->getParent()->getLeftChild() == &sentinel)
					{
						node->getParent()->setLeftChild(node);
						break;
					}
					node->setParent(node->getParent()->getLeftChild());
				}
				else if (node->getParent()->getRightChild() == &sentinel)
				{
					node->getParent()->setRightChild(node);
					break;
				}
				else
					node->setParent(node->getParent()->getRightChild());
			}
		}
		auto processNode = [&node, this](Node * x, void (DataTypes::TreeRedBlack::*rotate1)(Node *), void (DataTypes::TreeRedBlack::*rotate2)(Node *), DataTypes::TreeRedBlack::Node * (DataTypes::TreeRedBlack::Node::*child)() const) -> bool
		{
			if (x->getColor() == Red)
			{
				node->getParent()->setColor(Black);
				x->setColor(Black);
				node->getParent()->getParent()->setColor(Red);
				node = node->getParent()->getParent();
				return true;
			}
			else if (node == (node->getParent()->*child)())
			{
				node = node->getParent();
				(this->*rotate1)(node);
			}
			node->getParent()->setColor(Black);
			node->getParent()->getParent()->setColor(Red);
			(this->*rotate2)(node->getParent()->getParent());
			return false;
		};
		while (node != root && node->getParent()->getColor() == Red)
		{
			if (node->getParent() == node->getParent()->getParent()->getLeftChild())
			{
				if (processNode(node->getParent()->getParent()->getRightChild(), &DataTypes::TreeRedBlack::rotateLeft, &DataTypes::TreeRedBlack::rotateRight, &DataTypes::TreeRedBlack::Node::getRightChild))
					continue;
				break;
			}
			if (processNode(node->getParent()->getParent()->getLeftChild(), &DataTypes::TreeRedBlack::rotateRight, &DataTypes::TreeRedBlack::rotateLeft, &DataTypes::TreeRedBlack::Node::getLeftChild))
				continue;
			break;
		}
		root->setColor(Black);
	}
		
	TreeRedBlack::~TreeRedBlack()
	{
		if (root != nullptr && root != &sentinel)
			purge(root);
	}
}