#pragma once
#include "IBasicOperations.h"
namespace DataTypes
{
	///<summary>
	///Interfejs ujednolicaj¹cy operacjê w formie tablicowej 
	///</summary>
	__interface IArray : public IBasicOperations
	{
		void popFront();
		void popBack();
		void erase(long index);
		void pushFront(long value);
		void pushBack(long value);
		void insert(long index, long value);
	};
}