export class ArrayExtension<T> extends Array<T> {

    public get isEmpty(): Boolean {
        return !this || this.length < 1;
    }
    
}