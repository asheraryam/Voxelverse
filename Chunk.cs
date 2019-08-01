using Godot;
using System;

public class Chunk : Node
{

	Vector3 ChunkPos = new Vector3(0,0,0);
	OpenSimplexNoise Noise = new OpenSimplexNoise();
	SpatialMaterial Mat = new SpatialMaterial();
	MeshInstance MeshNode;
	StaticBody StaticNode;
	Godot.Collections.Dictionary BlockDict = new Godot.Collections.Dictionary();

	
	
    public override void _Ready()
    {
        Mat = (SpatialMaterial)ResourceLoader.Load("res://TextureMaterial.tres");
    }


	public Godot.Collections.Array GetTextureAtlasUVs(Vector2 Size, Vector2 Pos)
	{

		Vector2 Offset = new Vector2(Pos[0]/Size[0],Pos[1]/Size[1]);
		Vector2 One = new Vector2(Offset[0]+(1/Size[0]),Offset[1]+(1/Size[1]));
		Vector2 Zero = new Vector2(Offset[0],Offset[1]);
		Godot.Collections.Array Arr = new Godot.Collections.Array();
		Arr.Add(Zero);
		Arr.Add(One);
		return Arr;
	}

	public Godot.Collections.Array GetFace(string Orient,int X,int Y,int Z)
	{
		Godot.Collections.Array Vertices = new Godot.Collections.Array();
		Godot.Collections.Array UVs = new Godot.Collections.Array();
		Vector2 TextureAtlasSize = new Vector2(8,8);
		if (Orient == "top")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(X,1+Y,Z));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			Vertices.Add(new Vector3(1+X,1+Y,1+Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
		}
		else if (Orient == "bottom")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(X,Y,1+Z));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			Vertices.Add(new Vector3(X,Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			Vertices.Add(new Vector3(1+X,Y,Z));
			Vertices.Add(new Vector3(X,Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
		}
		else if (Orient == "left")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(X,Y,1+Z));
			Vertices.Add(new Vector3(X,Y,Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			Vertices.Add(new Vector3(X,Y,Z));
			Vertices.Add(new Vector3(X,1+Y,Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
		}
		else if (Orient == "right")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(1+X,Y,Z));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			Vertices.Add(new Vector3(1+X,1+Y,1+Z));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
		}
		else if (Orient == "front")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(X,Y,1+Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			Vertices.Add(new Vector3(1+X,Y,1+Z));
			Vertices.Add(new Vector3(X,1+Y,1+Z));
			Vertices.Add(new Vector3(1+X,1+Y,1+Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
		}
		else if (Orient == "back")
		{
			Godot.Collections.Array UVOffsets = GetTextureAtlasUVs(TextureAtlasSize,new Vector2(0,0));
			Vertices.Add(new Vector3(1+X,Y,Z));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			Vertices.Add(new Vector3(X,Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			Vertices.Add(new Vector3(X,Y,Z));
			Vertices.Add(new Vector3(1+X,1+Y,Z));
			Vertices.Add(new Vector3(X,1+Y,Z));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[1]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[0]))[0],((Vector2)(UVOffsets[0]))[1]));
			UVs.Add(new Vector2(((Vector2)(UVOffsets[1]))[0],((Vector2)(UVOffsets[0]))[1]));
		}
		Godot.Collections.Array Arr = new Godot.Collections.Array();
		Arr.Add(Vertices);
		Arr.Add(UVs);
		return Arr;
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

}
