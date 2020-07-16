# scaffold
Bob's unauthorized scaffolding tool for Microsoft Learn Modules and Units.

You will need a very specific folder structure ... this is hard coded for now.  You should use the 1.0 release available here:

https://github.com/bobtabor-msft/scaffold/releases/tag/1.0

Unzipping this to your `C:\` drive will create all the folders, executables, and templates you need to get started.  Modify any template file you want to change out the stuff that gets generated.

Here's a typical interaction:

```console
C:\scaffold\bin>scaffold
Hi, I scaffold Learn modules!

First, what is the module's UID?
whatever-you-want

Next, please enter each unit UID, starting with 'introduction', selecting [Enter] to add another.
When I see 'summary' I'll know you're done.
Type 'quit' to exit before generating any files.
introduction
first-module
first-exercise
second-module
second-exercise
knowledge-check
summary

Working ...

Created c:\scaffold\output\whatever-you-want
Created c:\scaffold\output\whatever-you-want\media
Created c:\scaffold\output\whatever-you-want\includes
Created the index.yml
Created c:\scaffold\output\whatever-you-want\1-introduction.yml
Created c:\scaffold\output\whatever-you-want\2-first-module.yml
Created c:\scaffold\output\whatever-you-want\3-first-exercise.yml
Created c:\scaffold\output\whatever-you-want\4-second-module.yml
Created c:\scaffold\output\whatever-you-want\5-second-exercise.yml
Created c:\scaffold\output\whatever-you-want\6-knowledge-check.yml
Created c:\scaffold\output\whatever-you-want\7-summary.yml
Created c:\scaffold\output\whatever-you-want\includes\1-introduction.md
Created c:\scaffold\output\whatever-you-want\includes\2-first-module.md
Created c:\scaffold\output\whatever-you-want\includes\3-first-exercise.md
Created c:\scaffold\output\whatever-you-want\includes\4-second-module.md
Created c:\scaffold\output\whatever-you-want\includes\5-second-exercise.md
Created c:\scaffold\output\whatever-you-want\includes\7-summary.md
Created the reference.txt

Success!

Next steps:
Your scaffolded module is saved at: c:\scaffold\output\whatever-you-want
Copy this to your local Learn-PR repo
```
