    1. When does Git perform a fast-forward and when is a merge commit created? 
        We can nerge fast-forward when main branch hasn't changed since the feature branch diversion and we can just stack up commits on top of main.
        If it is not the case, we have to create a merge commit to resolve conflicts and manage changes on both branches
    2. What is the practical difference between merge and rebase? 
        Merge forces us to overwrite changes that were made on main since a feature-branch broke off. 
        Rebasing allows us to move the break-away history forward to the current main commit and do a fast-forward merge as if nothing happened
    3. How was the conflict resolved in your repository? 
        I chose 2 different numbers as outputs for a function, I chose the option I put on main instead of the feature-branch one
