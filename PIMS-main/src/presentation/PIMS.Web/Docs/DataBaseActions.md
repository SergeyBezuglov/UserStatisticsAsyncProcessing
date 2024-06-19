# ���������� �� ������ � ��
## �������� �������� ��
������ ���������� �������� �� ��� ��
```
add-migration InitialCreate -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```

## ���������� ��
������ ��������� ��� ��
```
update-database -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
```
