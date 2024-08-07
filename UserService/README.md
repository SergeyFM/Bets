# UserService
**UserService**: будет предоставлять функционал UserService через RESTful API.   
**IdentityServer**: Этот проект будет хостить IdentityServer4, отвечающий за выдачу JWT токенов и обработку аутентификации и авторизации.
[Подробнее об архитектуре UserService](./UserService_Architecture.md)
Сущности:   
**User**: Основная сущность пользователя.  
**Role**: Роли, которые может иметь пользователь.  
**UserRole**: Связь между пользователями и ролями.  
**UserClaim**: Дополнительные утверждения для пользователей.  
**UserLogin**: Внешние логины пользователей (например, через Google, Facebook и т.д.).  
**UserToken**: Токены для двухфакторной аутентификации или восстановления пароля.  
**UserProfile**: Дополнительная информация о пользователе, такая как имя, фамилия, дата рождения, фото, документы
**Permission**: Разрешения, связанные с ролями или пользователями.  
**AuditLog**: Запись журнала аудита действий пользователя для обеспечения безопасности и отслеживания активности.