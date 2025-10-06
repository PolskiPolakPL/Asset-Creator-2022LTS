# Random Tickets Utilities 1.0

**Description:**  
This utility provides a flexible system for creating weighted tickets (`RandomTicket`) with adjustable weight and a "misses" counter.  
It can be used for a variety of purposes, such as balancing player selection in asymmetrical games, controlling the probability of triggered events, and assigning player roles while ensuring fairness and scalability. 

## Practical Scenarios
1. **Tracking chances for a player to become "it" in an asymmetrical game of tag:**
   - The longer a player is not chosen, the higher their chance becomes (by increasing their ticket's weight), balancing opportunities for all players to be selected.

2. **Controlling the probability of events triggered by a game action:**  
   - One trigger can initiate multiple possible events, with adjustable probability for each event in a highly scalable way.

3. **Assigning player roles in asymmetrical games:**  
   - For example, if the number of detectives is limited to 2, exactly two random players will become detectives.  
   - Tickets are drawn from a list of players, and a ticket selected for a player is immediately removed from the pool to prevent duplicates.

## Contains
- **RandomTicket.cs**: Class representing weighted tickets with adjustable weight and a "misses" counter.
- **ExampleScript.cs**: Example MonoBehaviour demonstrating RandomTicket usage with Demo1 and Demo2.

## Usage
1. **Create instances of RandomTicket in your scripts:**  
   - You can give them a name, starting weight, minimum and maximum weight.  
     Example:  
     ```cs
     RandomTicket myTicket = new RandomTicket("Red Ticket", 3, 1, 5);
     ```

2. **Perform a weighted random selection:**  
   - Keep track of the total weight of all tickets.  
     Example:  
     ```cs
     int totalWeight = ticket1.Weight + ticket2.Weight + ticket3.Weight;
     ```  
   - Generate a random number in the range `[0, totalWeight)`.  
   - Subtract each ticket's weight from the random number until it goes below zero.  
   - The ticket that causes the number to drop below zero is the selected ticket.

3. **Adjust weights dynamically if needed:**  
   - Use `Add(int amount)` or `Subtract(int amount)` to modify ticket weight.  
   - Reset weight to `MinWeight` with `SetWeight(int value)` if required.

4. **Track misses:**  
   - The public `misses` field can be incremented when a ticket is not chosen.  
   - You can increase weight based on misses to favor tickets that have been skipped.

5. **Display or log information:**  
   - Use `ToString()` to get a readable description of a ticket.  
     Example:  
     ```cs
     Debug.Log(myTicket.ToString());
     ```

## Notes
- Demo scripts are for demonstration purposes.  
- `RandomTicket` supports `MinWeight`, `MaxWeight`, and automatic weight adjustment on misses.
- AI tools were used solely to generate code comments and draft the general structure of this README.

---

© 2025 PolskiPolakPL. All rights reserved.