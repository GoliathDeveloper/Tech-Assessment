# Tech-Assessment

Thought process:
The way I approached this was using the example of Spin -> Spot, I then picked another example of code -> dote, so first writing down all the words in the file that had a length of 4 including and between the two words.

Then with this dataset I could then narrow down the search where all the words only had 1 letter difference, sort them alphabetically so they were in order and add them to a seperate list for a lookup.

e.g. using my example of code -> dote
Incorrect: Code -> Coke -> Come -> Dome -> Dote
Correct: Code -> Come -> Dome -> Dote

Finally I used a foreach loop and dictionary to work out which items from start to end and ensured they met my expectations.

I implemented some additional logic to calculate the least amount of differences and finally ended with the word I expected.

Testing/Performance:
In the solution you will see I have already implemented some tests covering the code to 87%.

I have not covered the Console application itself or the Wrapped Console class as these have calls to the methods, interfaces and classes that are already covered by the unit tests.
 
I might have gone a bit overboard on the comments, summaries but from previous experience people can sometimes get lost with my linq so have to explain logic.

As regards to performance there might be a minor performance increase either using a SortedSet or HashSet and minimising the output messages, but I see this being very minimal.
