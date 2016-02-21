# SocialNetwork

### Posting
A User has the ability to Post via commandline input, in the form `{username} -> {message}`.

The User is created if it doesn't already exist.

### Reading
A User has the ability to see their Posts by typing their name at the input. For example:

`> Liam -> everything is awesome!`

`> Liam`

`everything is awesome! (0 minutes ago)`

### Following
A User can follow another User by inputting `{User} follows {User}`.

`> Liam -> everything is awesome!`

`> Joe -> everything could be awesome, research not complete`

`> Liam follows Joe` -- Liam is now following Joe

### Wall
A User has the ability to see all Posts from itself and Users it is following via the Wall command.

`> Liam -> everything is awesome!`

`> Joe -> everything could be awesome, research not complete`

`> Liam follows Joe`

`> Sarah -> new research indicates some things may not be awesome`

`> Liam follows Sarah`

`> Liam wall`

`Sarah - new research indicates some things may not be awesome (0 minutes ago)`

`Joe - everything could be awesome, research not complete (5 minutes ago)`

`Liam - everything is awesome! (7 minutes ago)`

### Classes
**User** - holds metadata for each User

**Post** - holds a message content and time of the post.

**Timeline** - In-memory store for Posts

**Commands** - A collection of static methods to deal with all possible user commands


----------

### Build instructions
- Clone the repository to your machine.

- Either
  - Open **SocialNetwork.sln** in Visual Studio
  - Hit F5
- Or, if you don't want to use VS
  - In a command window
  - `C:\Windows\Microsoft.NET\Framework\{your .NET version}\MSBuild "C:\Path\To\SocialNetwork\SocialNetwork.sln"`
  - `C:\Path\To\SocialNetwork\SocialNetwork\bin\{Debug or Release}\SocialNetwork.exe`