# BasicCommon.Utilities
Include common helper classes like json, excel, enum helpers.


## Excel Helper
Export excel file with given model. Builds header with properties 'Description' attribute. If there are 'enum' values in given model, helper gets value from 'Description' attribute.

- Dependency: ClosedXML (>= 0.95.4)

## Enum Helper
Returns a value of 'description' attribute or KeyValuePair list with enum values.