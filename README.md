# Case Study: Event Bus with Sender Filtering

I created this repository as a case study for Albert upon request. It was about giving the listeners in an Event Bus the ability to filter for the sender.

Even though I was asked to deliver only one solution, I created three solutions. I decided to keep them all and rank them by preference.

## Solutions (Higher on the list is better):

### 1. Channels

We create a flags enum that describes event contexts. Senders need to describe their contexts. Listeners need to describe which context they want to listen to. Since we use an enum with flags, we can define composite contexts.

This is the best solution in terms of long-term maintenance. It requires library consumers to write more code than with the other options. However, once specified, the sender's context doesn't unintentionally change during refactors as opposed to other solutions. Since we decouple the context from the sender's identity, we can refactor contexts separately. We also have the option of mixing and matching contexts in the form of composite flag enum entries, without ever touching the sender site.

Pros:
* Strongly typed contexts.
* Ability to create composite contexts very easily.
* Refactor-friendly.
* No reflection.
* No code-gen.

Neutral:
* Ability to introduce arbitrary contexts.

Cons:
* Requires additional code on sender site.


### 2. Namespace Contexts Hardcode

On the listener site, we pass in a namespace as our context, using strings. Senders don't need to specify anything extra. Upon emit, we reflect upon the sender to find its namespace.

This is a worse solution than channels because there is hardcoding. It is possible to make typos in the string and it would not cause any problems until runtime. If namespaces change during refactoring and not the strings, again, it would not cause any compilation errors.

Pros:
* Ease of use for the library consumer.
* No extra code required on the sender site.

Neutral:
* Can fake support for composite channels on listener site by taking in a params array for contexts.

Cons:
* Hardcoding - possibility for errors due to typos.
* Not refactor-friendly.
* Uses reflection.


### 3. Namespace Contexts Code Generation

We generate code for (somewhat) strongly typing the namespaces. On the listener site, we pass in the generated object that represents the namespace. Upon emit, we reflect upon the sender to find its namespace.

This is a promising solution, with two very big problems:

1. If the namespaces change during a refactor, there is no support for tracking down the listener sites and changing them too. While something like this might be technically possible with IDE tooling, in my opinion, it is not worth the effort.

2. If there is a compilation error, code generation doesn't run. I guess this is solvable by extracting the code generation into a DLL (maybe a T4 library), but again, I don't think it is worth the additional maintenance overhead. Also, even then, I don't think namespaces would be fresh upon generation since the code wouldn't compile.

Pros:
* Somewhat strongly-typed.
* Ease of use for the library consumer.
* No extra code required on the sender site.

Neutral:
* Can fake support for composite channels on listener site by taking in a params array for contexts.

Cons:
* Not refactor-friendly.
* Uses reflection.
* Unity has no easy way of doing code generation.

## Opinions and Closing

I am always a proponent of not tying the hands of a developer in an effort of preventing them from making mistakes. 

I don't think targeting namespaces for filtering, which means coupling the channels with the sender's identity, brings any substantial benefit to the table. On top of that, if we allow for arbitrary channels, one can easily fake namespace-targeted filtering by creating a context for each namespace. Arbitrary channels allow for very easy (and reusable) creation of composite contexts that is effectively hidden away from the library consumer.

I also don't like using reflection and code generation unless they are the only solution to the problem. They are slow, clunky, magic, and they cause headaches. The first solution doesn't use either of them.

Due to the reasons I mentioned, "1. Channels" solution is my pick for the best solution for this case study.
