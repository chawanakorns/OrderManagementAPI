# ระบบจัดการคำสั่งซื้อ (Order Management API)

โปรเจคนี้คือ Web API ฝั่ง Backend ที่สร้างขึ้นด้วย .NET 8 สำหรับจัดการข้อมูลลูกค้า, สินค้า, และคำสั่งซื้อ โปรเจคนี้ถูกพัฒนาขึ้นเพื่อเป็นส่วนหนึ่งของการทดสอบความรู้ทางเทคนิค เพื่อแสดงทักษะด้านการพัฒนา API

## คุณสมบัติ (Features)

- **RESTful API:** มี Endpoints สำหรับจัดการคำสั่งซื้อ ทั้งการสร้างและดึงข้อมูล
- **การเชื่อมต่อฐานข้อมูล:** ใช้ Entity Framework Core (ในรูปแบบ Code-First) เพื่อติดต่อกับฐานข้อมูล SQL Server
- **โครงสร้างฐานข้อมูลที่ชัดเจน:** ออกแบบฐานข้อมูลเชิงสัมพันธ์ (Relational Database) ที่มีโครงสร้างชัดเจน ประกอบด้วย 3 ตารางหลักคือ `Customers`, `Products`, และ `Orders`
- **เอกสาร API:** มี Swagger/OpenAPI ในตัว ช่วยให้สามารถสำรวจและทดสอบ API ได้อย่างง่ายดาย
- **การใช้งาน Dependency Injection:** ใช้หลักการปฏิบัติที่ดีที่สุด (Best Practices) ในการจัดการ Dependencies เช่น การฉีด `DbContext` เข้าไปใช้งาน

## เทคโนโลยีที่ใช้ (Technology Stack)

- **เฟรมเวิร์ก:** ASP.NET Core 8
- **ภาษา:** C#
- **ORM:** Entity Framework Core 8
- **ฐานข้อมูล:** Microsoft SQL Server
- **เอกสาร API:** Swagger (Swashbuckle)

## โครงสร้างฐานข้อมูล (Database Schema)

ฐานข้อมูลประกอบด้วย 3 ตารางหลักที่ถูกออกแบบมาให้เรียบง่ายและมีความสัมพันธ์กัน

1.  ตาราง **`Customers`**: ใช้สำหรับเก็บข้อมูลลูกค้า
    - `CustomerID` (Primary Key, INT, Identity)
    - `FirstName` (NVARCHAR)
    - `LastName` (NVARCHAR)
    - `Email` (NVARCHAR, Unique)
    - `IsActive` (BIT)
    - `RegisteredDate` (DATETIME)

2.  **ตาราง `Products`**: ใช้สำหรับเก็บข้อมูลสินค้า
    - `ProductID` (Primary Key, INT, Identity)
    - `ProductName` (NVARCHAR)
    - `Price` (DECIMAL)
    - `IsAvailable` (BIT)
    - `DateAdded` (DATETIME)

3.  **ตาราง `Orders`**: เป็นตารางสำหรับเก็บข้อมูลการดำเนินการสั่งซื้อ และเชื่อมความสัมพันธ์ระหว่างลูกค้ากับสินค้า
    - `OrderID` (Primary Key, INT, Identity)
    - `CustomerID` (Foreign Key ของ `Customers`)
    - `ProductID` (Foreign Key ของ `Products`)
    - `OrderDate` (DATETIME)
    - `Quantity` (INT)
    - `IsShipped` (BIT)

### ความสัมพันธ์ระหว่างตาราง (Relationships)

- `Customer` หนึ่งคน สามารถมีได้หลาย `Orders` (One-to-Many)
- `Product` หนึ่งชิ้น สามารถอยู่ในหลาย `Orders` ได้ (One-to-Many)
- `Order` หนึ่งรายการ จะเป็นของ `Customer` เพียงคนเดียว และอ้างอิงถึง `Product` เพียงชิ้นเดียว