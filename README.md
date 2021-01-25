The project is opensource, but it is reliant one some external plugins (that are opensource as well, I hope, otherwise I'm in a lot of trouble)

# XFM Simulator
## Theme
Dedicated to r/rickygervais reddit community. Full of references from the 20 year old tinpot radio show hosted by Ricky Gervais, Steve Merchant and a little round headed Manc with a head like an fucking orange called Karl Pilkington. Without context it might seem far fetched and bizarre (Monkey driving off to Spain?). That's ok, it doesn't make more sense even with the context.

## Usability
Despite the twaddle that the content is, the architecture is fairly generic, could be used as a foundation for a game with similar mechanics.

Third party addons used
- Extenject
- UniRx
- DOTween (free version)
- RangedFloat and RangedInt from [heisarzola Unity Development Tools](https://github.com/heisarzola/Unity-Development-Tools "heisarzola's Unity Development Tools")

## Architecture and patterns and misc notes
- Dependency injection (Extenject, see [documentation](https://github.com/svermeulen/Extenject "documentation")). For my implementation see SceneContext game onject in GameScene and Installers folder in Scripts folder.
- Observable (UniRx, see [documentation](https://github.com/neuecc/UniRx "documentation")). For my implementation look in any of the Views, such as `CardView` (that's the one with most logic) or Controllers (`CardController`)
- DOTween - all animations are in a form of a Tweener (see `ActorShakeTweener`) with values always in a corresponding (or generic) **config file** (see `ActorShakeTweenerConfig`). No magic numbers, no values on the instances of the classes. Tweeners are called by views (there might be an exceptions)
- SOLID (more or less) - UniRx and Extenject are great help for that, help you to easily keep the classes and features decoupeled, open for extension, etc.

## Content
Almost all balancing and visuals are managed in Scriptable Objects (called Configs in my implementation)
### Examples:
- `CompetitionConfig` - has a pool of opponents that are being drawn at the start of the game.

to be continued when I can be arsed
