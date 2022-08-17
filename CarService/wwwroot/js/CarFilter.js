export default {
    props: {
        SearchOptions: Object,
    },
    data() {
        return {
            SearchTerms: {
                MinPrice: '',
                MaxPrice: '',
                Company: [],
                Model: [],
                Color: []
            },
        };
    },
    methods: {
        search() {
            console.log(this.SearchTerms);
            this.$emit("filter", this.SearchTerms);
        }
    },
    template: `
        <div class="row align-items-center">
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="card-title">
                            <h6 class="text-muted text-uppercase">Narxlar</h6>
                        </div>
                        <div class="card-text">
                            <div class="g-2 mb-1">
                                <label class="control-label">Minimum: </label>
                                <input class="form-control" v-model="SearchTerms.MinPrice" placeholder="Minimum" />
                            </div>
                            <div class="col-md">
                                <label class="control-label">Maximum: </label>
                                <input class="form-control" v-model="SearchTerms.MaxPrice" placeholder="Maximum" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="card-title">
                            <h6 class="text-muted text-uppercase">Kompaniyalar</h6>
                        </div>
                        <div class="card-text">
                            <div class="form-check" v-for="comp in SearchOptions.Companies">
                                <input class="form-check-input" type="checkbox" :id="comp" :value="comp" v-model="SearchTerms.Company">
                                <label class="form-check-label" :for="comp">{{comp}}</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="card-title">
                            <h6 class="text-muted text-uppercase">Modellar</h6>
                        </div>
                        <div class="card-text">
                            <div class="form-check" v-for="model in SearchOptions.Models">
                                <input class="form-check-input" type="checkbox" :id="model" :value="model" v-model="SearchTerms.Model">
                                <label class="form-check-label" :for="model">{{model}}</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mb-3 rounded-4">
                    <div class="card-body">
                        <div class="card-title">
                            <h6 class="text-muted text-uppercase">Ranglar</h6>
                        </div>
                        <div class="card-text">
                            <div class="form-check" v-for="color in SearchOptions.Colors">
                                <input type="checkbox" class="form-check-input" :id="color" :value="color" v-model="SearchTerms.Color">
                                <label class="form-check-label" :for="color">{{color}}</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <input type="submit" value="Filtr" v-on:click="search()" class="btn btn-primary w-50" />
                </div>
            </div>
    `,
};