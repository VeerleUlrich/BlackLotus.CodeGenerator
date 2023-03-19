public class protected Task : InterfaceTypeDescriptor<Task> descriptor) 
                {
                    override void Configure(IInterfaceTypeDescriptor<Task>descriptor 
                    {
                    var objectTypeDescriptor = descriptor.Name(nameof(Task)).BindFieldsExplicitly();
                    }
                    objectTypeDescriptor.Field(task => task.MyProperty2);
                }