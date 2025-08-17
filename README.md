# CEIS400-Final-Team5
CEIS400 Team 5: Equipment Checkout System

How to download, run, and grade the app

What this app does

Login (Main Form): Select an employee and enter password.

Checkout/Return: After login, view your current tools, return one, or check out a new tool.

Reports: View all open checkouts and Export to CSV.

Admin Panel (Admin only): Manage employees (add, terminate/restore, grant/revoke admin).

A test Admin account is included:
User: Admin User Password: 0000
(Admin-only controls appear automatically after an Admin logs in.)


1) Requirements

Windows 10/11

.NET Framework 4.7.2 or newer (Visual Studio 2022 Community OK)

Microsoft Access Database Engine (ACE OLE DB) installed
If you see “provider not registered” at runtime, install the Microsoft Access Database Engine 2016 Redistributable (64-bit).
(If your Office install is 32-bit, either use the 32-bit ACE or install the 64-bit redistributable using the passive switch.)

You do not need Microsoft Access itself; just the ACE provider.


2) Get the code

Option A — Download ZIP

Go to the GitHub repo.

Click Code → Download ZIP and extract it.

Option B — Clone

git clone https://github.com/BlankFrank94/CEIS400-Final-Team5.git


If you’re evaluating new Admin features, switch to our test branch in GitHub (named in the repo). Otherwise, use the default branch.


3) Open & run in Visual Studio

Open Team5-Final.sln.

Build with Any CPU (or x64 if you installed the 64-bit ACE provider).

Database file location: The Access DB file CEIS400Team5DB.accdb is included in the project.

In Solution Explorer, click the DB file → Properties → ensure
Copy to Output Directory = Copy if newer.
This ensures the database is placed next to the built EXE.

Press F5 to run.


4) Using the app
Login (Main Form)

Pick an employee from the dropdown.

Enter password and click Login.

Admin User / 0000 is provided for grading the Admin panel.

The app blocks login for terminated employees.

Checkout / Return screen

My Tools (grid): shows what the signed-in user has checked out right now.

Return: pick a tool from the dropdown → Return.

Checkout: pick an available tool → Checkout.
The app enforces skill level requirements and prevents double-checkout of the same item.

Reports

Open Report to see all open checkouts across the system.

Click Export CSV to save the grid to a .csv file for grading.

Admin panel (Admins only)

Manage Employees (buttons are enabled only for Admins):

Add Employee (auto-generates an EmployeeID).

Grant/Revoke Admin.

Terminate Employee (prevents further logins).

Restore Employee (sets back to Active/User).

Changes appear immediately in the grid.


5) What’s in the code (high level)

Data access: DataManager.cs (OleDb / Access SQL)

Business rules: InventoryService.cs

UI / Forms:

MainForm.cs (login + navigation)

CheckoutReturnForm.cs (user’s tools, checkout, return)

ReportForm.cs (open checkouts + CSV export)

ManageEmployeesForm.cs (admin controls)

ManageInventoryForm.cs (if included in your branch)

Current user context: CurrentUser.cs

Entry point: Program.cs


6) Test accounts & data

Admin User / 0000

Other employees are preloaded in the database; each has a skill level for tool eligibility.

The database tracks DateCheckedOut and DateReturned for the log.

The Report reflects all items where DateReturned is NULL.

Employee Accounts: 
Alex Berry: bFQA26Sq
Brian Sanderson: GcYQQ3KC
David Hunter: nH2KenMx
Jonathan Robertson: wHmUnMMq
Jacob Turner: mbFPQLgH
Leonard Newman: cx2SGLEF
Mark Peters: 4r8tH8ns
Michael Randall: DzQ7RsYh
Owen Wilkins: WSQAkakH
Paul Lawrence: BTr84H6w
Ryan Vaughan: TF2By8BZ
Stephen Avery: 6NnDsF7Y
Simon Wright: tRzvMCCG
Victor Howard: tF4yvCsS
Warren Mitchell: gqmxmMH7


7) Troubleshooting

“Provider not registered” → Install the ACE OLE DB engine (match the bitness you’re running).

“No value given for one or more required parameters” → Usually a table/column name mismatch.
Tables used by the app (do not rename):

[Employees Table] (contains: EmployeeID, FirstName, LastName, SkillLevel, Role, JobStatus, PasswordHash/Salt if present)

[Equipment Table] (contains: EquipmentID, EquipmentName, MinSkillLevel)

EquipmentLogTable (contains: LogID, EmployeeID, EquipmentID, DateCheckedOut, DateReturned, optional IsDamaged/IsLost)

DB not found → Confirm CEIS400Team5DB.accdb is present in the project and set to Copy if newer so it lands in bin\Debug (or bin\Release) next to the EXE.


8) Grading checklist (quick)

Launch app → Login screen appears.

Login as Admin User / 0000.

Open Manage Employees (visible only for Admins).
Add/terminate/restore, and grant/revoke admin to verify role gating.

Logout, login as a regular employee.

On Checkout/Return, verify:

Can’t check out a tool above the user’s skill.

Can’t check out a tool that’s already out.

Items returned immediately disappear from “My Tools”.

Open Report and Export CSV to confirm file download.
