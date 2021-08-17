# Thank you for the oppotunity 
# By Brandon Pearman

## considerations
Not thread safe at the moment, would need to add locks/concurrent dictionary and similar stuff. Alternatively could use something like Akka but would not want to add extra dependencies on a library which would force it on the developers using it.
In general would prefer to use as little dependencies in the library as possible for that reason.

## Performance
While performance was kept in mind no performance tests are included. Benchmarks could be run to get an idea of its performance, but benchmarks real value would be best over long term to see if new changes affected the performance.
Instead of creating an actual int[][] which would be more memory intensive, I opted for only storing the boundaries and check those.

## Correctness and stability
Making sure the library followed the spec as close as possible was the highest priority so unit testing was emphasised with
100% code coverage
100% mutants killed (stryker)

Classes own their own data and dont allow incorrect state.

## Maintainability
Kept classes and methods to resonable sizes with the highest cycolmatic complexity of 4
Tried to keep code verbose so that it is clear and easily understood.
Tried to name as clear as possible. (always the hardest part)

## User Experience
Comments on public code which may require description.
Exception thrown to maintain consistency are display in the description of the methods.
Code that users should not use are internal.

The classes are built to support many scenarios which the user may wish to do.
Different overloads were created for the users preference.

Libraries can sometimes be difficult to get started with so a small how to can guide its developers.
Should expand documentation and public method comments as/when developers struggle with the use of the library.

# How to

1. Create a RocketLandingCoordinatorMain object

2. Call its AddPlatform method

3. Check your rocket with Check method


# Other Tools used

## stryker Mutations
https://stryker-mutator.io/docs/stryker-net/getting-started
- goto command prompt at unit test location
- Install tool with "dotnet tool restore"
- Runwith "dotnet stryker"
- See StrykerOutput folder for details
- modify with config "stryker-config.json"
