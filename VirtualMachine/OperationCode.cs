namespace Cruel.VirtualMachine
{
	public enum OperationCode
	{
		NoOp,
		Load,
		LoadI,
		Save,
		SaveI,
		Branch,
		BranchI,
		BranchIfTrue,
		BranchIfTrueI,
		BranchIfFalse,
		BranchIfFalseI
	}
}