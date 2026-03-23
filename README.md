Class3:
    Instructions to run: run the file Program.cs (in the IDE or in the terminal) and enter all the parameters in a single like, space separated

    Design decisions:
    1. I tried to stick with SOLID practice: each class has its own responsibility: being a piece of equipment, a user, handling rentals, ect.
    2. The majority of business logic and constraints is implemented directly in the Program.cs file, where the CLI implementation dwells.
    It was made for ease of modification.
    3. The data entered into the CLI is contained withing the process memory and
    is not committed to any database for the sake of simplicity. In production application a database is of course required
    4. I aimed for high cohesion and responsibility splits, gathering duties related to different functionalities into different classes
    5. I also aimed for loose coupling as the classes' interactions are usually just referals to other objects.
    As the logic of handling duties of different classes is implemented fully inside the classes,
    I reached loose coupling as any class can be modified and no other functionality will break except possibly for the CLI interface
    
Class2:

    1. When does Git perform a fast-forward and when is a merge commit created? 
        We can nerge fast-forward when main branch hasn't changed since the feature branch diversion and we can just stack up commits on top of main.
        If it is not the case, we have to create a merge commit to resolve conflicts and manage changes on both branches
    2. What is the practical difference between merge and rebase? 
        Merge forces us to overwrite changes that were made on main since a feature-branch broke off. 
        Rebasing allows us to move the break-away history forward to the current main commit and do a fast-forward merge as if nothing happened
    3. How was the conflict resolved in your repository? 
        I chose 2 different numbers as outputs for a function, I chose the option I put on main instead of the feature-branch one
