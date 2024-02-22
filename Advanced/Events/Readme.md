## Events
When using delegates, two emergent roles commonly appear: broadcaster and subscriber. \
The broadcaster is a type that contains a delegate field. The broadcaster decides
when to broadcast, by invoking the delegate.

The subscribers are the method target recipients. A subscriber decides when to start
and stop listening by calling += and -= on the broadcaster’s delegate. A subscriber
does not know about, or interfere with, other subscribers. 

Events are a language feature that formalizes this pattern. An event is a construct
that exposes just the subset of delegate features required for the broadcaster/subscriber model. The main purpose of events is to prevent subscribers from interfering
with one another.

```
// Delegate definition
public delegate void PriceChangedHandler (decimal oldPrice, decimal newPrice);

public class Broadcaster
{
 // Event declaration
 public event PriceChangedHandler PriceChanged;
}
```

### Standard Event Pattern
In almost all cases for which events are defined in the .NET libraries, their definition adheres to a standard pattern designed to provide consistency across library
and user code. At the core of the standard event pattern is System.EventArgs, a pre‐
defined .NET class with no members (other than the static Empty field). EventArgs
is a base class for conveying information for an event. In our Stock example, we
would subclass EventArgs to convey the old and new prices when a PriceChanged
event is fired:
```
public class PriceChangedEventArgs : System.EventArgs
{
 public readonly decimal LastPrice;
 public readonly decimal NewPrice;
 
 public PriceChangedEventArgs (decimal lastPrice, decimal newPrice)
 {
    LastPrice = lastPrice;
    NewPrice = newPrice;
 }
}
```
With an EventArgs subclass in place, the next step is to choose or define a delegate
for the event. There are three rules: 

- It must have a void return type. 
- It must accept two arguments: the first of type object and the second a
subclass of EventArgs. The first argument indicates the event broadcaster, and
the second argument contains the extra information to convey. 
-  Its name must end with EventHandler. 

##### .NET defines a generic delegate called System.EventHandler<> to help with this:
