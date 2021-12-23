# NameGen
The purpose of this CLI tool is very simple: generate names when given how many syllables you need.

# Instructions
Simply run the program as follows: `namegen {syllables} {flags}` \
\
The flags are as follows:
```
-c  : (Continious)   - Keeps the program alive and allows you to continiously hit enter to get more names.
-pg : (Pre-Generate) - Pregenerates all of the possible combinations so that there is no delay when going through many possibilities.
-d  : (Debug) Simply - outputs how long everything takes in ms, with or without pregeneration.
```

# Todo
- Run pregeneration on multiple threads to speed up pregeneration.
