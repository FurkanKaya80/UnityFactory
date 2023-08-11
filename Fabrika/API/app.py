from flask import Flask, jsonify, request
import psycopg2
from flask_cors import CORS, cross_origin

app = Flask(__name__)
cors = CORS(app)


@app.route("/")
def index():
     return "API is working.."



###
app.route("/addData", methods=['GET', 'POST'])
def addData():
   form = next(iter(request.form.values()))
   form = form.split("&")
   for value in form:
      value = value.split("=")
      if(value[0] == "hostName"):
         hostName = value[1]
      if(value[0] == "userID"):
         userID = value[1]
      if(value[0] == "password"):
         password = value[1]
      if(value[0] == "databaseName"):
         databaseName = value[1]
      if(value[0] == "tableName"):
         tableName = value[1]
      if(value[0] == "columnName"):
         columnName = value[1]
      if(value[0] == "value"):
         veri = value[1]
   db = psycopg2.connect(user="postgres", password="12345", host=
   "localhost", database="Deneme")
   cursor = db.cursor()
   cursor.execute("INSERT INTO \"" + "makine" +  "\"(\"" + "material" + "\") VALUES('{0}')".format(str(veri)))
   db.commit()
   cursor.close()
   db.close()
   return "The Data Was Inserted Successfully"

###

"""@app.route("/getData", methods=['GET', 'POST'])
def getData():
   form = (iter(request.form.values()))
   #form = form.split("&")
   for value in form:
      value = value.split("=")
      if(value[0] == "hostName"):
         hostName = value[1]
      if(value[0] == "userID"):
         userID = value[1]
      if(value[0] == "password"):
         password = value[1]
      if(value[0] == "databaseName"):
         databaseName = value[1]
      if(value[0] == "tableName"):
         tableName = value[1]
      if(value[0] == "columnName"):
         columnName = value[1]
   
   db = psycopg2.connect(user = "postgres", password = "12345", host =
   "localhost", database = "Deneme")
   cursor = db.cursor()
   #cursor.execute("SELECT* \"" + "title" + "\" FROM \"" + "makine" + "\"")
   cursor.execute("SELECT * FROM \"" + "makine" + "\"ORDER BY id")
   values = cursor.fetchall()

   cursor.close()
   db.close()
 
   return jsonify(values)


if __name__ == '__main__':
    app.debug = True
    app.run()
"""

@app.route("/getData", methods=['GET', 'POST'])
def getData():
   form_data = request.form.to_dict()

   hostName = form_data.get("hostName", "")
   userID = form_data.get("userID", "")
   password = form_data.get("password", "")
   databaseName = form_data.get("databaseName", "")
   tableName = form_data.get("tableName", "")
   columnName = form_data.get("columnName", "")

   db = psycopg2.connect(user="postgres", password="12345", host="localhost", database="Deneme")
   cursor = db.cursor()

   # Sütun adını columnName olarak kullan
   cursor.execute("SELECT * FROM \"" + "makine" + "\"ORDER BY id")
   values = cursor.fetchall()

   cursor.close()
   db.close()

   return jsonify(values)


if __name__ == '__main__':
   app.run(debug=True)
   host = "0.0.0.0"; #Localde kullanım için
