# Components

``` uml
@startuml
allow_mixing
package "Application [F#]"        as application         #d4f1f8 {
  package "Baker"                 as baker               #d4f1f8 {}
  package "Controller"            as controller          #d4f1f8 {}
}
package "Datasource [C#]"         as datasource          #d0ebd2 {}
package "Datasource.Adaptor [F#]" as datasource_adaptor  #d4f1f8 {}
package "Service [F#]"            as service             #d4f1f8 {}
package "Domain [F#]"             as domain              #d4f1f8 {}
database "Data Store"             as datastore

baker              --> controller
baker              --> datasource
baker              --> datasource_adaptor
baker              --> service
baker              --> domain
controller         --> service
controller         --> domain
datasource         --> datastore
datasource_adaptor <-- datasource
datasource_adaptor --> service
datasource_adaptor --> domain
service            --> domain
@enduml
