#pragma once
#include "IBasicOperations.h"
namespace DataTypes
{
	///<summary>
	///Interfejs definiujący operacje na drzewach 
	///</summary>
	__interface ITree : public IBasicOperations
	{
		void add(long key);
		void erase(long key);
	};
}