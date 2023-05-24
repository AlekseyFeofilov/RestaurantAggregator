
# RestaurantAggregator - Феофилов Алексей

## Что нужно для того, чтобы задеплоить приложение

1. Чтобы подключить RabbitMQ нужно запустить контейнер в Docker с помощью команды 
    > docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
1. Чтобы подключить БД, можно вызвать `docker-compose up` из *RestaurantAggregator.Backend.DAL/DbDeploy/* (с помощью данного compose файла поднимуться обе бд: для Auth и Backend компонентов)
1. Чтобы заполнить БД для компонента Backend данными, можно запустить SQL файл из *RestaurantAggregator.Backend.DAL/DbDeploy/SQL/*
    - Предостережение: я забыл исправить, чтобы меню и блюда генерировались по дефолту активными, так что после того, как запустите SQL, надо будет залезть в бд и поставить у меню и у блюд Active = true

## Как потыкать

Потыкать можно в коллекции Postman

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/24099978-3c50d98a-16b8-4a94-ad7e-2b5d436fc716?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D24099978-3c50d98a-16b8-4a94-ad7e-2b5d436fc716%26entityType%3Dcollection%26workspaceId%3Da16bb9e1-2aa6-4a71-b903-47aa6c41b818)

На всякий случай есть ещё [ссылка](https://www.postman.com/gold-rocket-241630/workspace/restaurantaggregator/collection/24099978-3c50d98a-16b8-4a94-ad7e-2b5d436fc716?action=share&creator=24099978)

На всякий случай х2 есть ещё json файлы коллекций
1. *RestaurantAggregator.Auth.API.postman_collection.json* 
2. *RestaurantAggregator.Backend.API.postman_collection.json*

## Что я сделал крутого (помимо самого ТЗ)

- ***Храню конфигурации в appsettings и использую их через IOptions*** (*RestaurantAggregator.Auth.API/appsettings.json*, *RestaurantAggregator.Backend.API/appsettings.json*)


- ***Кастомные политики авторизации*** (*RestaurantAggregator.Backend.API/Extensions/AddServices.cs*)


- ***Парамеритризованные политики авторизации через AuthorizationPolicyProvider*** (*RestaurantAggregator.Backend.API/AuthorizationConfigurations/AuthorizationPolicyProviders/OrderStatusChangerPolicyProvider.cs*)


- ***Если менеджер меняет/удаляет блюдо, а оно уже использовалось в каком либо заказе, то вместо изменения старого блюда, будет создано новое, а старое будет доступно только для просмотра в истории заказов*** (*RestaurantAggregator.Backend.BL/Repositories/DishRepository.cs*)


- ***Partition View*** (*RestaurantAggregator.AdminPanel/Views/Shared*)


- ***Кастомные Middlaware*** (*RestaurantAggregator.Backend.API/Middleware*, *RestaurantAggregator.Auth.API/Middleware*)
  - ***для обработки исключений***
  - ***для трансофрмации результата*** (при преобразовании 403 ошибки псоле зафейленной авторизации во что-то более подходящее)


- ***Использовал Identity + UserManager/RoleManager*** (*RestaurantAggregator.Auth.API/RestaurantAggregator.Auth.API/*)


- ***Использовал паттерн репозиториев*** (*RestaurantAggregator.Common/CrudRepository*, *RestaurantAggregator.Auth.BL/Repositories*, *RestaurantAggregator.Backend.DAL/IRepositories*)


- ***Реализовал Notification модуль с RabbitMQ и SingulR*** (*RestaurantAggregator.Notification/RestaurantAggregator.Notification.csproj*)


- ***Многослойная архитектура***


- ***Разделяю data классы в API, BL и DAL на model-dto-entity*** (*RestaurantAggregator.Backend.API/Models*, *RestaurantAggregator.Backend.Common/Dtos*, *RestaurantAggregator.Backend.DAL/Entities*)


- ***Кастомные атрибуты валидации*** (*RestaurantAggregator.Backend.API/AuthorizationConfigurations/AuthorizationAttributes*)


- ***Кастомные исключения*** (*RestaurantAggregator.Common/Exceptions*, *RestaurantAggregator.Backend.Common/Exceptions*, *RestaurantAggregator.Auth.Common/Exceptions*)


- ***Использую auto mapping*** (*RestaurantAggregator.Backend.BL/MapperProfiles*, *RestaurantAggregator.Backend.API/MapperProfiles*, *RestaurantAggregator.Auth.BL/MapperProfiles*, *RestaurantAggregator.Auth.API/MapperProfiles*)


## Что я не сделал

- crud меню для менеджера
- нельзя оставить отзыв
- повторение предыдущего заказа
- всякие удобства по типу скрытия неактивных блюд для менеджера или отображение заказов по статусам
- где-то недорефакторил