# food-delivery-company
1)A food order tree will be created for the neighborhoods in Bornova.
Each node of the tree will hold the name of a neighborhood and the list of orders made from that neighborhood (OrdersList).
Each order in this list will be kept as a list (Order Information). Each element of this list will be a FoodClass object.
The FoodClass must contain the name, quantity and unit price of the ordered food or beverage.
[The tree should be created as a binary search tree based on the neighborhood name.]
  a) Creating the Food Order Tree:
     For each of the 5 neighborhoods in Bornova (Evka 3, Özkanlar, Atatürk, Erzene, Kazımdirik), create an order list with a random number of orders between 5-10.
   Get 3-5 random food/drink type information in each order. Food quantities can vary between 1 and 8 randomly.
   To generate an order, you can first create a list with menu information and use it to randomly pull the food/beverage name and price.
   b) Write the method that finds the depth of the tree and lists all the information in the tree (based on the order information in each neighborhood) to the screen.
   c) Write the method that lists the information of orders over 150 TL in a named neighborhood.
   d) Write a method that returns how many (not by how many people) ordered a given food/beverage in the entire tree.
   Update by applying 10% discount to the unit price of the food in all the lists where that food is mentioned in the tree.
2)
  a) Write the code that places the Information of the 10 neighborhoods you selected in Bornova into a Hash Table according to the Neighborhood Name.
  b) Write the code that updates the Hash Table by adding 1 to the total population of the neighborhoods whose initials are given.
3)
  a) Learn Heap Data Structure and its methods. Code and run with C#. You can use array or List to hold elements in infrastructure.
  b) Max. 10 neighborhoods in Bornova according to their population. Write the code that embeds the heap.
  c) Extract the 3 neighborhoods with the highest population from the Heap, and write the code that lists the relevant neighborhood name and population information.
