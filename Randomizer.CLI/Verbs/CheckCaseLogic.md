# Motivation

Figuring out what combination of progression are minimal unique subsets for a
given logical expression can be cumbersome. This tool is designed to find
duplicates and proper subsets after the initial work of unfolding the logical
expression into separate lines.

# Usage

`Randomizer.CLI checkcaselogic [File]`

`File`
:   Specify the path to a text file containing one logic case per line.

The procedure has two stages, an initial duplicate scan followed by a subset
match scan. Each stage is completely finished before continuing to the next.

If a stage found matches the lines will be updated with prefixes to describe
these matches. The prefixes are removed if both stages pass. The console will
report which stage was reached, or if there was a success.

Note that the subset scan only match a line with one other line, so multiple
edit-check cycles might be required to find all issues.

# File content

Each (non-empty) line represent one case. Only inventory progression is
supported, leaving out the Has (and HasIn), Assume, and IfSkippingAny/AlsoSkip
constructs. It does support the leveled items `MasterSword`, `Mitt`, and
`TwoPowerBombs`, as well as higher counts than one (which is the default
count). The following is an example of a line:

```
Super Gravity MoonPearl Flippers Morph MasterSword Cape Lamp KeyCT(2) Bombs
```

If duplicates are found those lines will be prefixed by an "x":

```
x Super Gravity ...
```

If subsets are found those lines will be prefixed by a "<N" for the subset, and
">N" for the superset, for some pair number N.

```
<1 Morph Bombs Super
>1 SpeedBooster Super Morph Bombs
```

These prefixes can safely remain in the file between edits since the tool
identify them before it scan the line contents.
