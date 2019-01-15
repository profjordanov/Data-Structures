# Array Data Structures

- Ordered
- Very lightweight
- Has a fixed size
- Usually built into the language
- Many collections are implemented by using arrays
- Arrays use a single block of memory

  Example: (Int32 uses 4 bytes)

  ```csharp
  int[] array = { 2, 4, 1, 3, 5 };
  ```
  Total: 5 * 4 bytes

- Uses total of array pointer + (N * element/pointer size)
- Array Address + (Element Index * Size) = Element Address
- Array Element Lookup – O(1)
- Memory after the array may be occupied
- If we want to resize the array we have to make a copy
- Array Copy – O(n)

# List Data Structure

- Has a variable size
- Implemented using an array
- Dynamic Arrays (Lists) – Add O(1)
- Doubles its capacity when needed
- Copying occurs at log(n)  n = 109, only ~30 copies

# Linked Lists
## Chain of Nodes
### Node
- Building block of many data structures
- A basic Node has a value and a pointer to the next node

  ```csharp
  Node<int> head = new Node(2);
  Node<int> next = new Node(5);

  head.Next(next);
  ```
  
