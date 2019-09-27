RuleEngine in .net Core

This Project is a really simple rule engine maker that you can define your facts and develop the main functionality of rules that you are free to choose the method name.
You just need to use our annotation to demonstrate actions and conditions.

## Rule
You must create a new instance of rule and pass your rule class as input then call `Execute` method.

## Facts
You can define your facts as an input value to rules. You can add in your facts in every step of rule action method also.

#### Usage
Create a new instance of facts and set key-value base all facts.
If you want to get a fact's value in condition but you don't define in facts it must raise an error.

## Annotations
- Action
- Condition
- Rule

### Action
This annotation can only set on `Methods`. You can define more than one action with priority for every rule.

#### Usage
- Return `Void`
- No Args as input
#### Properties
- Order : It's not requeried.Default value is `1`

### Condition
This annotation can only set on `Methods`. You can define more than one condition. After evaluates all condition rule engine executes all actions that you develop. 
#### Usage
- Return `Bool`
- Args as demonstrate in FactArgs with the same priority

#### Properties
- FactArgs : Comma separated facts name
- ExceptionMessage : The message must be raised as Rule Exception when this condition result it's not true

### Rule
This annotation can only set on the class. You can have more than one rule.
#### Usage
You must set on the class as an attribute that is your rule.
#### Properties
- Name : It's optional and you can set this rule name.
- Description : It's optional and you can set the main functionality of this rule.
- NextRule : It's optional and you can set the type of next rule that you have and used the rule attribute. Rule engine after running current rule actions evaluate the next one and execute if everything is going well.