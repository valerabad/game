using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
public class DrawLine : MonoBehaviour 
{
	bool flag;
	private LineRenderer line;
	private LineRenderer shapeLine;
	private bool isMousePressed;
	private List<Vector3> pointsList;
	private Vector3 mousePos;
	private Vector3 p;
	private bool isUpmouse;
	private TrailRenderer trLine;
	private List<Vector3> squareList;
	private List<float> rast;

	private List<float> xList;  private List<float> yList;
	private List<float> shabl_xList;  private List<float> shabl_yList;

	private List<Vector3> sampleList;



	private List<Vector3> MouseList;
	private Vector3 tmpMousePosition;
	private String ourString ;
	private List<Vector3> PointsMinMax;
	private List<Vector3> ML;
	private List<Vector3> SL;
	private List<Vector3> new_ML;
	private Vector3 sample;
	Vector3 start_draw_sample;
	List<float> distanse;
	float summa_dist;

	float beginkoordX;
	float beginkoordY;
	float Shabl_beginkoord_X;
	float Shabl_beginkoord_Y;
	int counter = 0;
	int number_levesl;
	int j;
	float sampl_fig,mouse_fig,koef;
	int countKoordInFile = 0;
	string[] result;
	string ourStr;
	string level_name;
	string[] levels = { "Square", "Triangle", "Rectangle","z","Sircle" };
	char[] charSeparators = new char[] { ',' };
	bool next_level;

	//	-----------------------------------	
	void Awake()  //событие выпоняется перед Start()
	{	
		line = gameObject.AddComponent<LineRenderer>();
		line.material =  new Material(Shader.Find("Particles/Additive"));
		line.SetVertexCount(0);

		line.SetWidth(0.1f,0.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;
		isMousePressed = false;
		isUpmouse = false;
		pointsList = new List<Vector3>();
		MouseList = new List<Vector3>();
		squareList = new List<Vector3>();
		PointsMinMax = new List<Vector3> ();
		ML= new List<Vector3>();
		SL = new List<Vector3> ();
		new_ML = new List<Vector3> ();
		xList = new List<float> ();		yList = new List<float> ();
		shabl_xList = new List<float> ();	shabl_yList = new List<float> ();
		sampleList = new List<Vector3> ();  
		distanse = new List<float> ();
		rast= new List<float> ();
	}
		
	void Start (){

		number_levesl = 0;
		//counter = 0;
		System.IO.StreamReader file3 = new System.IO.StreamReader(@"c:\test\"+levels[0]+".txt"); //Square
		while ((ourStr = file3.ReadLine()) != null) {
			countKoordInFile++;
		}
		line.SetVertexCount(countKoordInFile);
		System.IO.StreamReader file2 = new System.IO.StreamReader(@"c:\test\"+levels[0]+".txt");
		while ((ourStr = file2.ReadLine()) != null)
		{
			
			ourStr = ourStr.Substring(1,ourStr.Length-2);
			result = ourStr.Split(charSeparators, StringSplitOptions.None);
			result[0] = result[0].Trim(); shabl_xList.Add(Convert.ToSingle(result[0]));
			result[1] = result[1].Trim(); shabl_yList.Add(Convert.ToSingle(result[1]));
			result[2] = result[2].Trim();
			//Camera.main.ScreenToWorldPoint
			sample = (new  Vector3(Convert.ToSingle(result[0]), Convert.ToSingle(result[1]),Convert.ToSingle(result[2]) ));
			//sample.z=0.0f;
			sampleList.Add(sample); 
			line.SetPosition(counter,Camera.main.ScreenToWorldPoint(new Vector3( sample.x,sample.y,2.0f) ));
			counter++;
		}
	
		file3.Close();
		file2.Close();



		//tmpMousePosition = Input.mousePosition;
		GUI.Label (new Rect(200,200,200,50), "(Pixels)Input.mousePosition: "  );	
	}
	void OnGUI() {
		if (next_level==false)
		{
			GUI.TextArea (new Rect(10,10,200,50), " Уровень: "+levels[number_levesl] );
		}
		else 
		{
			GUI.Label (new Rect(200,200,200,50), "Вы победили!!!"  );
		}

	}
	void Update ()
	{

		//pointsList.RemoveRange (pointsList.Count);
		if(Input.GetMouseButtonDown(0))
		{
			isMousePressed = true;
			if (isUpmouse == false)
			  line.SetVertexCount(pointsList.Count);  // Set the number of line segments.
			pointsList.RemoveRange(0,pointsList.Count);// delete elements of list begining 0 index
			//testList.RemoveRange(0,testList.Count);
			line.SetColors(Color.green, Color.green);  // ?
		}
		else if(Input.GetMouseButtonUp(0))
		{
			line.SetVertexCount(0);

			mouse_fig=SearchCoord();
			sampl_fig = Shabl_SearchCoord ();


			Debug.Log (sampl_fig);
			Debug.Log (mouse_fig);
			if (sampl_fig>mouse_fig) {
				koef=mouse_fig/sampl_fig;
				TransSample(koef);
				TransMouseDraw(1);
			}
			else 
			{
				koef=sampl_fig/mouse_fig;
				TransSample(1);
				TransMouseDraw(koef);
			}
			Debug.Log(koef);
			//----------------------------------------------------------
			isMousePressed = false;
			isUpmouse = true;
			summa_dist=0;
		if (ML.Count > SL.Count)
			{
				flag = true;
				for (int j_sl=0; j_sl < ML.Count; j_sl++){
					for(j=0;j < SL.Count;j++)
					{
						rast.Add(Vector3.Distance(ML[j],SL[j_sl]));
					}
					rast.Sort();
					summa_dist=summa_dist+rast[0];
					rast.RemoveRange(0,rast.Count);
				}
				Debug.Log(summa_dist);
			
			}
			else
			{
				flag=false;
				for (int j_sl=0; j_sl < SL.Count; j_sl++){
					for(j=0;j < ML.Count;j++)
					{
						rast.Add(Vector3.Distance(ML[j],SL[j_sl]));
					}
					rast.Sort();
					summa_dist=summa_dist+rast[0];
					rast.RemoveRange(0,rast.Count);
				}
				Debug.Log(summa_dist);
			}
				if ((summa_dist<=1500)&&(summa_dist>=1000)&&(flag==false))
				{
				    number_levesl++;

					Debug.Log("Верно! Следующий уровень: "+levels[number_levesl]);
					level_name= levels[number_levesl];
					
					 next_level=true;

				}
						else 
				{
					Debug.Log("Неверно");
					 next_level=false;
				}



			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\test\2.txt", false))//
			{                                                                                            //@"c:\test\Sqare.txt"
				for (int i=0;i< ML.Count  ;i++)
				{

					file.WriteLine(Convert.ToString(ML[i])); // sorting
					//file.WriteLine("line.SetPosition ("+i+",new Vector3"+Convert.ToString(MouseList[i])+");");
				}	

			}

			// список SL содержит изменённый координаты (scale+transe) шаблонной фигуры
			//sampleList.RemoveRange(0,sampleList.Count);
			new_ML.RemoveRange(0,new_ML.Count);
			distanse.RemoveRange(0,distanse.Count);
			SL.RemoveRange(0,SL.Count);
			ML.RemoveRange(0,ML.Count);
				MouseList.RemoveRange(0,MouseList.Count); // следить за порядком выполнения
				xList.RemoveRange(0,xList.Count); 
				yList.RemoveRange(0,yList.Count);


			if ((next_level)&&(number_levesl!=4))
			{

				next_level=false;
				sampleList.RemoveRange(0,sampleList.Count);
				shabl_xList.RemoveRange(0,shabl_xList.Count);
				shabl_yList.RemoveRange(0,shabl_yList.Count);

				countKoordInFile = 0;
				//line.SetVertexCount(0);
				counter = 0;
				System.IO.StreamReader file_1 = new System.IO.StreamReader(@"c:\test\"+levels[number_levesl]+".txt"); //Square
				while ((ourStr = file_1.ReadLine()) != null) {
					countKoordInFile++;
				}
				line.SetVertexCount(countKoordInFile);
				System.IO.StreamReader file_2 = new System.IO.StreamReader(@"c:\test\"+levels[number_levesl]+".txt");
				while ((ourStr = file_2.ReadLine()) != null)
				{
					//Debug.Log (ourStr);
					if (ourStr.Trim().Length < 3)
						continue;
					ourStr = ourStr.Substring(1,ourStr.Length-2);
					result = ourStr.Split(charSeparators, StringSplitOptions.None);
					result[0] = result[0].Trim(); shabl_xList.Add(Convert.ToSingle(result[0]));
					result[1] = result[1].Trim(); shabl_yList.Add(Convert.ToSingle(result[1]));
					result[2] = result[2].Trim();
					//Camera.main.ScreenToWorldPoint
					sample = (new  Vector3(Convert.ToSingle(result[0]), Convert.ToSingle(result[1]),Convert.ToSingle(result[2])));
					//sample.z=0 	;
					sampleList.Add(sample); 
					//line.set
					line.SetPosition(counter,Camera.main.ScreenToWorldPoint(new Vector3( sample.x,sample.y,2.0f) ));
					counter++;
				}


				file_1.Close();
				file_2.Close();

			}

		}

		// Drawing line when mouse is moving(presses)
		if(isMousePressed)  // если кнопака мыши зажата
		{
			//line.SetVertexCount(0);
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Debug.Log(mousePos);
			mousePos.z=0;
			//if (!pointsList.Contains (mousePos)) 
			{  
				pointsList.Add (mousePos);
				line.SetVertexCount (pointsList.Count); // Set the number of line segments.
				line.SetPosition (pointsList.Count-1, (Vector3)pointsList [pointsList.Count - 1]); // Set the position of the vertex in the line			
			}
		 }
		if ((tmpMousePosition != (Input.mousePosition)) && (isMousePressed) )
		{
			//Vector3.Normalize(Input.mousePosition.x	);
			xList.Add( ( Input.mousePosition.x));
			yList.Add(Input.mousePosition.y);
			//Camera.main.ScreenToWorldPoint
			MouseList.Add((new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z))); //Vector3.Normalize Camera.main.ScreenToWorldPoint

			//Debug.Log(Input.mousePosition);

			tmpMousePosition = Input.mousePosition;
		}

	}
	int Searh_Min(List<float> dist){
		int index_min = 0;
		float min = dist [0];
		for (int i=1; i<dist.Count; i++)
			if (dist [i] < min) {
			  min = dist [i];
			  index_min= i;
			}
		return index_min ;
	}
	void TransSample(float k)
	{
		for (j=0;j<sampleList.Count;j++)
		{		
			 SL.Add(new Vector3((sampleList[j].x-Shabl_beginkoord_X)*k, 
			                   (sampleList[j].y-Shabl_beginkoord_Y)*k,
			                   sampleList[j].z));
			
		}
	}
	void TransMouseDraw(float k)
	{

		for (j=0;j<MouseList.Count;j++)
		{		
			ML.Add(new Vector3((MouseList[j].x-beginkoordX)*k, 
			                          (MouseList[j].y-beginkoordY)*k,
			                          MouseList[j].z));

		}
		//MouseList.RemoveRange(0,MouseList.Count);
		//-------------------------------------
	}
	float SearchCoord(){
		float storona1 = 0;
		float storona2 = 0;

		xList.Sort();
		yList.Sort();
		beginkoordX= xList[0];
		beginkoordY= yList[0];

		PointsMinMax.Add(new Vector3(xList[0], yList[0],0.0f));
		PointsMinMax.Add(new Vector3(xList[xList.Count-1], yList[0],0.0f));
		PointsMinMax.Add(new Vector3(xList[0], yList[yList.Count-1],0.0f));
		storona1 = Vector3.Distance(PointsMinMax[0],PointsMinMax[1]);	
		storona2 = Vector3.Distance(PointsMinMax[2],PointsMinMax[0]);
		Debug.Log(PointsMinMax[0]+" "+PointsMinMax[1]+" "+Vector3.Distance(PointsMinMax[0],PointsMinMax[1]));	
		Debug.Log(PointsMinMax[2]+" "+Vector3.Distance(PointsMinMax[2],PointsMinMax[0]));
		PointsMinMax.RemoveRange(0,PointsMinMax.Count);
		if (storona1 > storona2)
		{
			return storona1;
		}
		else 
			return storona2;
	}

	float Shabl_SearchCoord(){
		float storona1 = 0;
		float storona2 = 0;
		
		shabl_xList.Sort ();
		shabl_yList.Sort ();
		Shabl_beginkoord_X = shabl_xList [0];
		Shabl_beginkoord_Y = shabl_yList [0];

		PointsMinMax.Add(new Vector3(shabl_xList[0], shabl_yList[0],0.0f));
		PointsMinMax.Add(new Vector3(shabl_xList[shabl_xList.Count-1], shabl_yList[0],0.0f));
		PointsMinMax.Add(new Vector3(shabl_xList[0], shabl_yList[shabl_yList.Count-1],0.0f));

		storona1 = Vector3.Distance(PointsMinMax[0],PointsMinMax[1]);	
		storona2 = Vector3.Distance(PointsMinMax[2],PointsMinMax[0]);
		Debug.Log(PointsMinMax[0]+" "+PointsMinMax[1]+" "+Vector3.Distance(PointsMinMax[0],PointsMinMax[1]));	
		Debug.Log(PointsMinMax[2]+" "+Vector3.Distance(PointsMinMax[2],PointsMinMax[0]));
		PointsMinMax.RemoveRange(0,PointsMinMax.Count);
		if (storona1 > storona2)
		{
			return storona1;
		}
		else 
			return storona2;
	}
}