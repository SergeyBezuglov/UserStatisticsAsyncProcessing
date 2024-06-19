# Общая информация

## Именование тестов

T1 - Что мы тестируем <br>
T2 - Сценарий <br>
T3 - Ожидаемый результат
```csharp
public void T1_T2_T3()...
public void HandleRegisterCommandForms_WhenUserIsValid_ShouldCreateAndReturnAuthenticationResult()
```
## Дефолтное подключение к AD (добавляется в блок AuthenticationModules appsettings.json)
```json

  "AD": {
      "Type": "ActiveDirectory",
      "Name": "Active Directory",
      "Settings": {
        "Host": "devHost",
        "Domain": "dev.net",
        "AccessDomainUserName": "dev\\администратор",
        "AccessDomainUserPassword": "йцукен_123",
        "AllowNestedDomain": "true"
      }
    }
```
## Подход к именованию версий
```
<major>.<minor>.<patch>
```
