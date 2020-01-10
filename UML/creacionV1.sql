/*==============================================================*/
/* DBMS name:      ORACLE Version 12c                           */
/* Created on:     11/12/2019 21:12:29                          */
/*==============================================================*/


alter table LIBRO_CONTABLE
   drop constraint FK_LIBRO_CO_RELATIONS_USUARIO;

alter table PRESUPUESTO
   drop constraint FK_PRESUPUE_RELATIONS_USUARIO;

alter table REG_DET_LIBROS
   drop constraint FK_REG_DET__RELATIONS_TRANSACC;

alter table REG_DET_LIBROS
   drop constraint FK_REG_DET__RELATIONS_LIBRO_CO;

alter table REG_DET_PRESUPUESTOS
   drop constraint FK_REG_DET__RELATIONS_PRESUPUE;

alter table REG_DET_PRESUPUESTOS
   drop constraint FK_REG_DET__RREG_TRA_TRANSACC;

alter table TRANSACCION
   drop constraint FK_TRANSACC_RELATIONS_RUBRO;

alter table USUARIO
   drop constraint FK_USUARIO_RELATIONS_ROL;

drop index RELATIONSHIP_7_FK;

drop table LIBRO_CONTABLE cascade constraints;

drop index RELATIONSHIP_2_FK;

drop table PRESUPUESTO cascade constraints;

drop index RELATIONSHIP_6_FK;

drop index RELATIONSHIP_5_FK;

drop table REG_DET_LIBROS cascade constraints;

drop index RELATIONSHIP_4_FK;

drop index RELATIONSHIP_3_FK;

drop table REG_DET_PRESUPUESTOS cascade constraints;

drop table ROL cascade constraints;

drop table RUBRO cascade constraints;

drop index RELATIONSHIP_1_FK;

drop table TRANSACCION cascade constraints;

drop index RELATIONSHIP_8_FK;

drop table USUARIO cascade constraints;

/*==============================================================*/
/* Table: LIBRO_CONTABLE                                        */
/*==============================================================*/
create table LIBRO_CONTABLE (
   ID                   NUMBER(8)             not null,
   USU_ID               NUMBER(8)             not null,
   FECHA_INICIO         TIMESTAMP,
   FECHA_FIN            TIMESTAMP,
   TOTAL_INGRESOS       NUMBER(10,3)         default 0  not null,
   TOTAL_GASTOS         NUMBER(10,3)         default 0  not null,
   constraint PK_LIBRO_CONTABLE primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_7_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_7_FK on LIBRO_CONTABLE (
   USU_ID ASC
);

/*==============================================================*/
/* Table: PRESUPUESTO                                           */
/*==============================================================*/
create table PRESUPUESTO (
   ID                   NUMBER(8)             not null,
   USU_ID               NUMBER(8)             not null,
   FECHA_INICIO         TIMESTAMP,
   FECHA_FIN            TIMESTAMP,
   VALOR_PRE            NUMBER(10,3)         default 0  not null,
   TOTAL_GASTOS         NUMBER(10,3)         default 0,
   constraint PK_PRESUPUESTO primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_2_FK on PRESUPUESTO (
   USU_ID ASC
);

/*==============================================================*/
/* Table: REG_DET_LIBROS                                        */
/*==============================================================*/
create table REG_DET_LIBROS (
   ID                   NUMBER(10)            not null,
   TRA_ID               NUMBER(8),
   LIB_ID               NUMBER(8)             not null,
   constraint PK_REG_DET_LIBROS primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_5_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_5_FK on REG_DET_LIBROS (
   TRA_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_6_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_6_FK on REG_DET_LIBROS (
   LIB_ID ASC
);

/*==============================================================*/
/* Table: REG_DET_PRESUPUESTOS                                  */
/*==============================================================*/
create table REG_DET_PRESUPUESTOS (
   ID                   NUMBER(18)            not null,
   PRE_ID               NUMBER(8)             not null,
   TRA_ID               NUMBER(8)             not null,
   constraint PK_REG_DET_PRESUPUESTOS primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_3_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_3_FK on REG_DET_PRESUPUESTOS (
   TRA_ID ASC
);

/*==============================================================*/
/* Index: RELATIONSHIP_4_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_4_FK on REG_DET_PRESUPUESTOS (
   PRE_ID ASC
);

/*==============================================================*/
/* Table: ROL                                                   */
/*==============================================================*/
create table ROL (
   ID                   NUMBER(2)             not null,
   NOMBRE               VARCHAR2(32)          not null,
   constraint PK_ROL primary key (ID)
);

/*==============================================================*/
/* Table: RUBRO                                                 */
/*==============================================================*/
create table RUBRO (
   ID                   NUMBER(8)             not null,
   NOMBRE               VARCHAR2(200)         not null,
   constraint PK_RUBRO primary key (ID)
);

/*==============================================================*/
/* Table: TRANSACCION                                           */
/*==============================================================*/
create table TRANSACCION (
   ID                   NUMBER(8)             not null,
   RUB_ID               NUMBER(8)             not null,
   TIPO                 INTEGER               not null,
   FECHA                TIMESTAMP,
   SUBTOTAL             NUMBER(10,3),
   IMPUESTO             NUMBER(10,3),
   TOTAL                NUMBER(10,3),
   constraint PK_TRANSACCION primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_1_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_1_FK on TRANSACCION (
   RUB_ID ASC
);

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   ID                   NUMBER(8)             not null,
   ROL_ID               NUMBER(2)             not null,
   NOMBRE_USUARIO       VARCHAR2(60)          not null,
   NOMBRE               VARCHAR2(200)         not null,
   APELLIDO             VARCHAR2(200)         not null,
   CEDULA               VARCHAR2(10)          not null,
   RUC                  VARCHAR2(15)          not null,
   DIRECCION            VARCHAR2(200),
   TELEFONO             VARCHAR2(10),
   EMAIL                VARCHAR2(150),
   constraint PK_USUARIO primary key (ID)
);

/*==============================================================*/
/* Index: RELATIONSHIP_8_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_8_FK on USUARIO (
   ROL_ID ASC
);

alter table LIBRO_CONTABLE
   add constraint FK_LIBRO_CO_RELATIONS_USUARIO foreign key (USU_ID)
      references USUARIO (ID);

alter table PRESUPUESTO
   add constraint FK_PRESUPUE_RELATIONS_USUARIO foreign key (USU_ID)
      references USUARIO (ID);

alter table REG_DET_LIBROS
   add constraint FK_REG_DET__RELATIONS_TRANSACC foreign key (TRA_ID)
      references TRANSACCION (ID);

alter table REG_DET_LIBROS
   add constraint FK_REG_DET__RELATIONS_LIBRO_CO foreign key (LIB_ID)
      references LIBRO_CONTABLE (ID);

alter table REG_DET_PRESUPUESTOS
   add constraint FK_REG_DET__RELATIONS_PRESUPUE foreign key (PRE_ID)
      references PRESUPUESTO (ID);

alter table REG_DET_PRESUPUESTOS
   add constraint FK_REG_DET__RREG_TRA_TRANSACC foreign key (TRA_ID)
      references TRANSACCION (ID);

alter table TRANSACCION
   add constraint FK_TRANSACC_RELATIONS_RUBRO foreign key (RUB_ID)
      references RUBRO (ID);

alter table USUARIO
   add constraint FK_USUARIO_RELATIONS_ROL foreign key (ROL_ID)
      references ROL (ID);

create materialized view log on TRANSACCION;

