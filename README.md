# RuleEngineCore
RuleEngine in .net Core

This Project is a really simple rule engine maker that you can define your facts and develop the main functionality of rules that you are free to choose method name.
You just need use our annotation for demonestrate actions and conditions.

## Rule
You must create new instance of rule and pass your rule class as input then call `Execute` method

## Facts
You can define your facts as a input values to rules.

#### Usage
Create new instance of facts and set key value base all facts.
If you want to get a fact value in condition but you don't define in facts it must raise error.

## Annotations
- Action
- Condition
- Rule
- Next

### Action
This annoatation can set on `Methods` only. You can define more than one action with priority for every rule.
#### Usage
- Return `Void`
- No Args as input
#### Properties
- Order : It's not requeried.Default value is `1`

### Condition
This annotation can set on `Methods` only. You can define more than one condition. After evalutes all condition rule engine execute all actions that you develope. 
#### Usage
- Return `Bool`
- Args as demonostrate in FactArgs with same priority

#### Properties
- FactArgs: Comma seprated facts name
- ExceptionMessage: The message must be raised as Rule Exception when this conditon result it's not true

### Rule
This annotation can set on class. You can have more than one rule.
#### Usage
You must set on class as attribute that is your rule.
#### Properties
- Name : It's optional and you can set this rule name.
- Description : It's optional and you can set main functionality of this rule.

### Next
It's new feature to run another rule after evalute current rule. there is no any definition for this one :-)
#### Usage
#### Properties