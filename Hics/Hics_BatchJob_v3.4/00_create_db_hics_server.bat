@echo off
@set server=192.168.188.2
@set username=itin18_akt_admin
@set password=123User!
@set database=itin18_akt
@set logfile=logfile.log
@color 0A
echo Bitte den Pfad zum aktuellen Ordner angeben!
echo z.B. E:\Hics\Hics_BatchJob_v3.0
@set /p pfad=  

@set log_pfad=%pfad%/%logfile%

echo Angegebener Pfad 						[ Made by Lesin ;) ]> %log_pfad%
echo %pfad% >> %log_pfad%
echo. >> %log_pfad%

@set startzaehler=5
echo.
echo Es stehen %startzaehler% Datenbank Aufgaben an!
echo ------------------------------------ >> %log_pfad%
echo Es stehen %startzaehler% Datenbank Aufgaben an! >> %log_pfad%
echo. >> %log_pfad%
echo.
@call :einarbeiten1 "Dropping Objects" 01.1_delete_all_object.sql 1
@call :einarbeiten2 "Create Tables" 03_create_table_hics.sql 2
@call :einarbeiten2 "Create Primary Key" 04_create_pk_hics.sql 3
@call :einarbeiten2 "Create Foreigen Key" 05_create_fk_hics.sql 4
@call :einarbeiten2 "Create Index" 06_create_index_hics.sql 5

echo. >> %log_pfad%
echo Datenbank erfolgreich Eingearbeitet! >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
echo. >> %log_pfad%
echo. 
echo Datenbank erfolgreich Eingearbeitet!  

pause
cls

@set startzaehler=21
echo.
echo Es stehen %startzaehler% Funktions Aufgaben an! 
echo.
echo ------------------------------------ >> %log_pfad%
echo Es stehen %startzaehler% Funktions Aufgaben an! >> %log_pfad%
echo. >> %log_pfad%

cd %pfad%\Funktionen
@call :einarbeiten2 "fn_show_lamp_status" 18.1_fn_show_lamp_status.sql 1
@call :einarbeiten2 "fn_show_lampgroup_status" 18.2_fn_show_lampgroup_status.sql 2
@call :einarbeiten2 "fn_show_lamps" 18.3.1_fn_show_lamps.sql 3
@call :einarbeiten2 "fn_show_deleted_lamps" 18.3.2_fn_show_deleted_lamps.sql 4
@call :einarbeiten2 "fn_show_lampgroups" 18.4.1_fn_show_lampgroups.sql 5
@call :einarbeiten2 "fn_show_deleted_roomgroups" 18.4.2_fn_show_deleted_roomgroups.sql 6
@call :einarbeiten2 "fn_check_user_table" 19.2_fn_check_user_table.sql 7
@call :einarbeiten2 "fn_show_users" 19.3.1_fn_show_users.sql 8
@call :einarbeiten2 "fn_show_deleted_users" 19.3.2_fn_show_deleted_users.sql 9
@call :einarbeiten2 "fn_show_usergroups" 19.4.1_fn_show_usergroups.sql 10
@call :einarbeiten2 "fn_show_deleted_usergroups" 19.4.2_fn_show_deleted_usergroups.sql 11
@call :einarbeiten2 "fn_hash_password" 19.5_fn_hash_password.sql 12
@call :einarbeiten2 "fn_check_admin_table" 19.6_fn_check_admin_table.sql 13
@call :einarbeiten2 "fn_check_user_scalar" 19.7.1_fn_check_user_scalar.sql 14
@call :einarbeiten2 "fn_check_admin_scalar" 19.7.2_fn_check_admin_scalar.sql 15
@call :einarbeiten2 "fn_show_lamp_control" 19.8.1_fn_show_lamp_control.sql 16
@call :einarbeiten2 "fn_show_lamp_control_history" 19.8.2_fn_show_lamp_control_history.sql 17
@call :einarbeiten2 "fn_show_lampgroup_allocate" 19.8.3_fn_show_lampgroup_allocate.sql 18
@call :einarbeiten2 "fn_show_lamp_status_scalar" 19.9_fn_show_lamp_status_scalar.sql 19
@call :einarbeiten2 "fn_show_lamp_brightness_scalar" 19.10_fn_show_lamp_brightness_scalar.sql 20
@call :einarbeiten2 "fn_check_lamp_delete_scalar" 19.11_fn_check_lamp_delete_scalar.sql 21


echo. >> %log_pfad%
echo Funktionen erfolgreich Eingearbeitet! >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
echo. >> %log_pfad%
echo. 
echo Funktionen erfolgreich Eingearbeitet!  

pause
cls

@set startzaehler=20
echo.
echo Es stehen %startzaehler% Prozedur Aufgaben an! 
echo.
echo ------------------------------------ >> %log_pfad%
echo Es stehen %startzaehler% Prozedur Aufgaben an! >> %log_pfad%
echo. >> %log_pfad%

cd..
cd %pfad%\Prozeduren
@call :einarbeiten2 "sp_delete_data_from_tables" 1.2_sp_delete_data_from_tables.sql 1
@call :einarbeiten2 "sp_insert_test_data" 1.3_sp_insert_test_data.sql 2
@call :einarbeiten2 "sp_add_lamp" 2.1_sp_add_lamp.sql 3
@call :einarbeiten2 "sp_delete_lamp" 3.1_sp_delete_lamp.sql 4
@call :einarbeiten2 "sp_add_lampgroup" 4.1_sp_add_lampgroup.sql 5
@call :einarbeiten2 "sp_add_lamp_to_lampgroup" 5.1_sp_add_lamp_to_lampgroup.sql 6
@call :einarbeiten2 "sp_delete_lamp_from_roomgroup" 6.1_sp_delete_lamp_from_roomgroup.sql 7
@call :einarbeiten2 "sp_delete_roomgroup" 7.1_sp_delete_roomgroup.sql 8
@call :einarbeiten2 "sp_add_usergroup" 8.1_sp_add_usergroup.sql 9
@call :einarbeiten2 "sp_add_user" 8.3_sp_add_user.sql 10
@call :einarbeiten2 "sp_delete_usergroup" 8.2_sp_delete_usergroup.sql 11
@call :einarbeiten2 "sp_delete_user.sql" 8.4_sp_delete_user.sql 12
@call :einarbeiten2 "sp_add_user_to_usergroup" 8.5_sp_add_user_to_usergroup.sql 13
@call :einarbeiten2 "sp_delete_user_from_usergroup" 8.6_sp_delete_user_from_usergroup.sql 14
@call :einarbeiten2 "sp_lamp_on" 13.1_sp_lamp_on.sql 15
@call :einarbeiten2 "sp_set_lamp_stat" 13.2_sp_set_lamp_stat.sql 16
@call :einarbeiten2 "sp_lamp_off" 14.1_sp_lamp_off.sql 17
@call :einarbeiten2 "sp_lamp_dimm" 15.1_sp_lamp_dimm.sql 18
@call :einarbeiten2 "sp_change_password" 19.1.1_sp_change_password.sql 19
@call :einarbeiten2 "sp_change_password_by_admin" 19.1.2_sp_change_password_by_admin.sql 20


echo. >> %log_pfad%
echo Prozeduren erfolgreich Eingearbeitet! >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
echo. >> %log_pfad%
echo. 
echo Prozeduren erfolgreich Eingearbeitet!  

pause
cls

@set startzaehler=1
echo.
echo Es steht %startzaehler% Berechtigungs Aufgabe an!
echo.
echo ------------------------------------ >> %log_pfad%
echo Es steht %startzaehler% Berechtigungs Aufgabe an! >> %log_pfad%
echo. >> %log_pfad%

cd..
@call :einarbeiten2 "Berechtigungen einarbeiten" 07_create_grant_hics_server.sql 1
echo. >> %log_pfad%
echo Berechtigungen erfolgreich Eingearbeitet! >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
echo. >> %log_pfad%
echo. 
echo Berechtigungen erfolgreich Eingearbeitet!  

pause
cls


echo ( Vorsicht, wenn sie die Test Daten nicht einspielen,  
echo   gibt es keinen Benutzer, der in die History eingetragen wird )
echo Moechten sie die Test Daten einspielen?(j / n)
SET /p wahl=
if '%wahl%' == 'n' goto Nein
if '%wahl%' == 'j' goto Ja
goto Ende



:einarbeiten1
@set /a startzaehler=%startzaehler%-1
echo %1 >> %log_pfad%
osql -S %server% -U %username% -P %password% -i %2 >> %log_pfad%
echo. >> %log_pfad%
echo %1 
echo %3 Verarbeitet, bleiben noch %startzaehler%
echo.
goto :Ende

:einarbeiten2
@set /a startzaehler=%startzaehler%-1
echo %1 >> %log_pfad%
osql -S %server% -U %username% -P %password% -d %database% -i %2 >> %log_pfad%
echo. >> %log_pfad%
echo %1  
echo %3 Verarbeitet, bleiben noch %startzaehler%
echo.
goto :Ende

:Ja
@set startzaehler=1
cls
echo.
echo Es steht %startzaehler% Test Daten Aufgabe an!
echo.
echo ------------------------------------ >> %log_pfad%
echo Es steht %startzaehler% Test Daten Aufgabe an! >> %log_pfad%
echo. >> %log_pfad%

@call :einarbeiten2 "exec_main_data" 13_exec_main_data.sql 1
echo. 
echo Test Daten erfolgreich Eingearbeitet! 
echo. >> %log_pfad%
echo Test Daten erfolgreich Eingearbeitet! >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
 

pause
cls
goto :loop

:Nein
cls
echo.
echo Achtung!	Es wurden keine Test Daten eingearbeitet!
echo.

echo ------------------------------------ >> %log_pfad%
echo Achtung!	Es wurden keine Test Daten eingearbeitet! >> %log_pfad%
echo. >> %log_pfad%
echo ------------------------------------ >> %log_pfad%
pause
cls
goto :loop

:loop
@set /a time=6
@:loop_c
@set /a time=%time% -1
@if %time%==0 goto Ende
echo Fertig! 	Das Programm schliesst in %time% sek. automatisch
ping localhost -n 2 >nul
cls
goto loop_c

Goto Ende


:Ende